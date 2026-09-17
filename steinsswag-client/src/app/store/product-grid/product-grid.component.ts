import { Component, OnInit, signal, computed } from '@angular/core';
import { HttpErrorResponse } from '@angular/common/http';
import { Item } from '../../models/item.model';
import { Category } from '../../models/category.model';
import { ItemService } from '../../services/item.service';
import { CategoryService } from '../../services/category.service';
import { extractErrorMessage } from '../../utils/error-utils';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-product-grid',
  standalone: true,
    imports: [CommonModule],

  templateUrl: './product-grid.component.html',
  styleUrls: ['./product-grid.component.scss']
})
export class ProductGridComponent implements OnInit {
  items = signal<Item[]>([]);
  categories = signal<Category[]>([]);
  loading = signal(false);
  error = signal<string | null>(null);

  selectedCategoryId = signal<number | null>(null);

  filteredItems = computed(() => {
    const catId = this.selectedCategoryId();
    if (catId === null) return this.items();
    return this.items().filter(i => i.categoryId === catId);
  });

  constructor(
    private itemService: ItemService,
    private categoryService: CategoryService
  ) {}

  ngOnInit(): void {
    this.loading.set(true);
    this.error.set(null);

    this.itemService.getAll().subscribe({
      next: data => {
        this.items.set(data);
        this.loading.set(false);
      },
      error: (err: HttpErrorResponse) => {
        console.error('Failed to load items', err);
        this.error.set(extractErrorMessage(err, 'Failed to load items.'));
        this.loading.set(false);
      }
    });

    this.categoryService.getAll().subscribe({
      next: data => this.categories.set(data),
      error: (err: HttpErrorResponse) => console.error('Failed to load categories', err)
    });
  }

  selectCategory(id: number | null): void {
    this.selectedCategoryId.set(id);
  }
}