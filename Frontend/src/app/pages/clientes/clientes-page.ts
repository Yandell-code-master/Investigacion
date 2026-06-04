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

    clienteId = 0;

    modoEdicion = false;

    buscarId = 0;

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

        const cliente: Cliente = {

            id: this.clienteId,

            nombre

        };

        if (this.modoEdicion &&
            this.clienteId > 0) {

            this.clienteService
                .update(cliente)
                .subscribe(() => {

                    this.cancelarEdicion();

                    this.loadClientes();

                });

        }

        else {

            this.clienteService
                .create(cliente)
                .subscribe(() => {

                    this.nuevoNombre = '';

                    this.loadClientes();

                });

        }

    }

    buscarCliente(): void {

        if (!this.buscarId) {
            return;
        }

        this.clienteService
            .search(this.buscarId)
            .subscribe(cliente => {

                if (cliente.id > 0) {

                    this.clienteId =
                        cliente.id;

                    this.nuevoNombre =
                        cliente.nombre;

                }

            });

    }

    cancelarEdicion(): void {

        this.modoEdicion = false;

        this.clienteId = 0;

        this.buscarId = 0;

        this.nuevoNombre = '';

    }

    editarCliente(cliente: Cliente): void {

    this.modoEdicion = true;

    this.clienteId = cliente.id;

    this.nuevoNombre = cliente.nombre;

}



    deleteCliente(id: number): void {

        if (!confirm(
            '¿Eliminar cliente?'
        )) {

            return;

        }

        this.clienteService
            .delete(id)
            .subscribe(() => {

                this.loadClientes();

            });

    }


}