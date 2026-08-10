import { Routes } from '@angular/router';
import { ItemListComponent } from './components/item-list/item-list.component';
import { SellerListComponent } from './components/seller-list/seller-list.component';
import { CategoryListComponent } from './components/category-list/category-list.component';
import { PlacementSlotListComponent } from './components/placement-slot-list/placement-slot-list.component';

export const routes: Routes = [
  { path: '', redirectTo: 'items', pathMatch: 'full' },
  { path: 'items', component: ItemListComponent },
  { path: 'sellers', component: SellerListComponent },
  { path: 'categories', component: CategoryListComponent },
  { path: 'placement-slots', component: PlacementSlotListComponent },
];