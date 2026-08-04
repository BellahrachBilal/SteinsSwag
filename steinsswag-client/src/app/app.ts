import { Component, signal } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { ItemListComponent } from './components/item-list/item-list.component';
import { SellerListComponent } from './components/seller-list/seller-list.component';
import { PlacementSlotListComponent } from './components/placement-slot-list/placement-slot-list.component';
import { CategoryListComponent } from './components/category-list/category-list.component';

@Component({
  selector: 'app-root',
  imports: [RouterOutlet , ItemListComponent, SellerListComponent,
     PlacementSlotListComponent, CategoryListComponent],
  templateUrl: './app.html',
  styleUrl: './app.scss'
})
export class App {
  protected readonly title = signal('steinsswag-client');
}
