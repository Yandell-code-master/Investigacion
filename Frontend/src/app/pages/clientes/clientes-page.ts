import { Component, inject, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { RouterLink } from '@angular/router';

import { ClienteService } from '../../services/cliente.service';
import { Cliente } from '../../models/cliente.model';

@Component({
    selector: 'app-clientes-page',
    templateUrl: './clientes-page.html',
    imports: [
        CommonModule,
        FormsModule,
        RouterLink
    ]
})
export class ClientesPage {

    private readonly clienteService =
        inject(ClienteService);

    readonly clientes =
        signal<Cliente[]>([]);

    nuevoNombre = '';

    constructor() {
        this.loadClientes();
    }

    loadClientes(): void {

        this.clienteService
            .list()
            .subscribe(clientes => {

                this.clientes.set(clientes);

            });

    }

    saveCliente(): void {

        const nombre =
            this.nuevoNombre.trim();

        if (!nombre) {
            return;
        }

        this.clienteService
            .create({
                id: 0,
                nombre
            })
            .subscribe(() => {

                this.nuevoNombre = '';

                this.loadClientes();

            });

    }
}