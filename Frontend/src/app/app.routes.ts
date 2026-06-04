import { Routes } from '@angular/router';

import { HomePage } from './pages/home/home-page';
import { ClientesPage } from './pages/clientes/clientes-page';
import { ProductosPage } from './pages/productos/productos-page';
import { FacturasPage } from './pages/facturas/facturas-page';

export const routes: Routes = [

  {
    path: '',
    component: HomePage
  },

  {
    path: 'clientes',
    component: ClientesPage
  },

  {
    path: 'productos',
    component: ProductosPage
  },

  {
    path: 'facturas',
    component: FacturasPage
  },

  {
    path: '**',
    redirectTo: ''
  }

];