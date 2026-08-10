import { Component, OnInit, signal } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpErrorResponse } from '@angular/common/http';
import { Category, CreateCategory } from '../../models/category.model';
import { CategoryService } from '../../services/category.service';
import { extractErrorMessage } from '../../utils/error-utils';

@Component({
  selector: 'app-category-list',
  standalone: true,
  imports: [FormsModule],
  templateUrl: './category-list.component.html',
})
export class CategoryListComponent implements OnInit {
  categories = signal<Category[]>([]);
  loading = signal(false);
  error = signal<string | null>(null);

  newCategory: CreateCategory = this.emptyCategory();

  constructor(private categoryService: CategoryService) {}

  ngOnInit(): void {
    this.loadCategories();
  }

  loadCategories(): void {
    this.loading.set(true);
    this.error.set(null);

    this.categoryService.getAll().subscribe({
      next: data => {
        this.categories.set(data);
        this.loading.set(false);
      },
      error: (err: HttpErrorResponse) => {
        console.error('Failed to load categories', err);
        this.error.set(extractErrorMessage(err, 'Failed to load categories. Please try again.'));
        this.loading.set(false);
      }
    });
  }

  addCategory(): void {
    if (!this.newCategory.name) return;

    this.categoryService.create(this.newCategory).subscribe({
      next: () => {
        this.loadCategories();
        this.newCategory = this.emptyCategory();
      },
      error: (err: HttpErrorResponse) => {
        console.error('Failed to add category', err);
        this.error.set(extractErrorMessage(err, 'Failed to add category. Please try again.'));
      }
    });
  }

  private emptyCategory(): CreateCategory {
    return { name: '' };
  }
}