import { Routes } from '@angular/router';
import { ItemListComponent } from './components/item-list/item-list.component';
import { SellerListComponent } from './components/seller-list/seller-list.component';
import { CategoryListComponent } from './components/category-list/category-list.component';
import { PlacementSlotListComponent } from './components/placement-slot-list/placement-slot-list.component';

export const routes: Routes = [
  { path: '', redirectTo: 'store', pathMatch: 'full' },

  // Admin
  { path: 'admin/items', component: ItemListComponent },
  { path: 'admin/sellers', component: SellerListComponent },
  { path: 'admin/categories', component: CategoryListComponent },
  { path: 'admin/placement-slots', component: PlacementSlotListComponent },

  // Storefront (public)
  { path: 'store', loadComponent: () => import('./store/store-home/store-home.component').then(m => m.StoreHomeComponent) },
];