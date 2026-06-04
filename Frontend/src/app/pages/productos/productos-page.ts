import { Component, inject, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';


import { ProductoService } from '../../services/producto.service';
import { Producto } from '../../models/producto.model';

@Component({
    selector: 'app-productos-page',
    standalone: true,
    templateUrl: './productos-page.html',
    imports: [
        CommonModule,
        FormsModule,
        
    ]
})
export class ProductosPage {


private readonly productoService =
    inject(ProductoService);

readonly productos =
    signal<Producto[]>([]);

nuevoCodigoInterno = '';

nuevoCodigoBarra = '';

nuevaDescripcion = '';

nuevoPrecioVenta = 0;

nuevaExistencia = 0;

modoEdicion = false;

constructor() {
    this.loadProductos();
}

loadProductos(): void {

    this.productoService
        .list()
        .subscribe(productos => {

            this.productos.set(productos);

        });

}

saveProducto(): void {

    const producto: Producto = {

        codigoInterno: this.nuevoCodigoInterno,

        codigoBarra: this.nuevoCodigoBarra,

        descripcion: this.nuevaDescripcion,

        precioVenta: this.nuevoPrecioVenta,

        existencia: this.nuevaExistencia

    };

    if (this.modoEdicion) {

        this.productoService
            .update(producto)
            .subscribe(() => {

                this.cancelarEdicion();

                this.loadProductos();

            });

    }
    else {

        this.productoService
            .create(producto)
            .subscribe(() => {

                this.cancelarEdicion();

                this.loadProductos();

            });

    }

}

editarProducto(producto: Producto): void {

    this.modoEdicion = true;

    this.nuevoCodigoInterno =
        producto.codigoInterno;

    this.nuevoCodigoBarra =
        producto.codigoBarra ?? '';

    this.nuevaDescripcion =
        producto.descripcion;

    this.nuevoPrecioVenta =
        producto.precioVenta;

    this.nuevaExistencia =
        producto.existencia;

}

cancelarEdicion(): void {

    this.modoEdicion = false;

    this.nuevoCodigoInterno = '';

    this.nuevoCodigoBarra = '';

    this.nuevaDescripcion = '';

    this.nuevoPrecioVenta = 0;

    this.nuevaExistencia = 0;

}

deleteProducto(codigoInterno: string): void {

    if (!confirm('¿Eliminar producto?')) {
        return;
    }

    this.productoService
        .delete(codigoInterno)
        .subscribe(() => {

            this.loadProductos();

        });

}


}
