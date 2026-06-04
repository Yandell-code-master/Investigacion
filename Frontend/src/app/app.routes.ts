import { Routes } from '@angular/router';
import { ClientesPage } from './pages/clientes/clientes-page';

export const routes: Routes = [
  {
    path: '',
    component: ClientesPage
  },
  {
    path: 'productos',
    component: ClientesPage
  },
  {
    path: 'facturas',
    component: ClientesPage
  }
];