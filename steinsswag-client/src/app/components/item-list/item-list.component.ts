import { Component, OnInit, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Item, CreateItem, SourcePlatform, ItemCondition } from '../../models/item.model';
import { Category } from '../../models/category.model';
import { ItemService } from '../../services/item.service';
import { CategoryService } from '../../services/category.service';
import { HttpErrorResponse } from '@angular/common/http';
import { extractErrorMessage } from '../../utils/error-utils';

@Component({
  selector: 'app-item-list',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './item-list.component.html'
})

export class ItemListComponent implements OnInit {
    items = signal<Item[]>([]);
    categories = signal<Category[]>([]);
    loading = signal(false);
    error = signal<string | null>(null);

    sourcePlatforms = Object.values(SourcePlatform);
    conditions = Object.values(ItemCondition);

    newItem: CreateItem = {
        name: '',
        price: 0,
        categoryId: 0,
        sourcePlatform: SourcePlatform.Taobao,
        condition: ItemCondition.New,
    };

    constructor(
        private itemService: ItemService,
        private categoryService: CategoryService    
    ) { }
    
    ngOnInit(): void {
        this.loadItems();
        this.categoryService.getAll().subscribe({
            next: data => this.categories.set(data),
            error: (err: HttpErrorResponse) => {
                console.error('Failed to load categories', err);
                this.error.set(extractErrorMessage(err, 'Failed to load categories. Please try again.'));
            }
        });
    }

    loadItems(): void {
        this.loading.set(true);
        this.error.set(null);

        this.itemService.getAll().subscribe({
            next : data => {
                this.items.set(data);
                this.loading.set(false);
            },
            error: (err: HttpErrorResponse) => {
                console.error('Failed to load items', err);
                this.error.set(extractErrorMessage(err, 'Failed to load items. Please try again.'));
                this.loading.set(false);
            }
        });
    }

    addItem(): void {
        if (!this.newItem.name || !this.newItem.categoryId) return;

        this.itemService.create(this.newItem).subscribe({
            next: () => {
                this.loadItems();
                this.newItem = this.emptyItem();
            },
            error: (err: HttpErrorResponse) => {
                console.error('Failed to add item', err);
                this.error.set(extractErrorMessage(err, 'Failed to add item. Please try again.'));
            }
        });
    }

    deleteItem(itemId: number, itemName: string): void {
    this.itemService.delete(itemId).subscribe({
        next: () => this.loadItems(),
        error: (err: HttpErrorResponse) => {
            console.error('Failed to delete item', err);
            this.error.set(`Could not delete "${itemName}" — it may have already been removed.`);
        }
    });
} 


    private emptyItem(): CreateItem {
        return {
        name: '',
        price: 0,
        categoryId: 0,
        sourcePlatform: SourcePlatform.Taobao,
        condition: ItemCondition.New,
        };
    }

}

