import { Component } from '@angular/core';
import { ProductGridComponent } from '../product-grid/product-grid.component';

@Component({
  selector: 'app-store-home',
  standalone: true,
  imports: [ProductGridComponent],
  template: `<app-product-grid></app-product-grid>`,
})
export class StoreHomeComponent {}