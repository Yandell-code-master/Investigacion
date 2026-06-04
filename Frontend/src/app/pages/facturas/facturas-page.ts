import { Component, inject, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { RouterLink } from '@angular/router';

import { FacturaService } from '../../services/factura.service';
import { Factura } from '../../models/factura.model';

@Component({
    selector: 'app-facturas-page',
    templateUrl: './facturas-page.html',
    imports: [
        CommonModule,
        FormsModule,
        RouterLink
    ]
})
export class FacturasPage {

    private readonly facturaService = inject(FacturaService);

    readonly facturas = signal<Factura[]>([]);

    // Campos del formulario
    clienteId = 0;
    total = 0;
    
    // Control de estado
    facturaId = 0;
    modoEdicion = false;

    constructor() {
        this.loadFacturas();
    }

    loadFacturas(): void {
        this.facturaService.list().subscribe(facturas => {
            this.facturas.set(facturas);
        });
    }

    saveFactura(): void {
        if (this.clienteId <= 0 || this.total < 0) {
            alert('Por favor ingrese un Cliente ID válido y un total.');
            return;
        }

        const factura: Factura = {
            id: this.facturaId,
            clienteId: this.clienteId,
            fechaEmision: new Date(), 
            total: this.total
        };

        if (this.modoEdicion && this.facturaId > 0) {
            this.facturaService.update(factura).subscribe(() => {
                this.cancelarEdicion();
                this.loadFacturas();
            });
        } else {
            this.facturaService.create(factura).subscribe(() => {
                this.limpiarFormulario();
                this.loadFacturas();
            });
        }
    }

    editarFactura(factura: Factura): void {
        this.modoEdicion = true;
        this.facturaId = factura.id;
        this.clienteId = factura.clienteId;
        this.total = factura.total;
    }

    deleteFactura(id: number): void {
        if (!confirm('¿Eliminar factura?')) return;

        this.facturaService.delete(id).subscribe(() => {
            this.loadFacturas();
        });
    }

    limpiarFormulario(): void {
        this.clienteId = 0;
        this.total = 0;
    }

    cancelarEdicion(): void {
        this.modoEdicion = false;
        this.facturaId = 0;
        this.limpiarFormulario();
    }
}