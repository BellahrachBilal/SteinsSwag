import { Component, OnInit, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Item, CreateItem, SourcePlatform, ItemCondition } from '../../models/item.model';
import { Category } from '../../models/category.model';
import { ItemService } from '../../services/item.service';
import { CategoryService } from '../../services/category.service';

@Component({
  selector: 'app-item-list',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './item-list.component.html'
})

export class ItemListComponent implements OnInit {
    items = signal<Item[]>([]);
    categories = signal<Category[]>([]);

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
        this.categoryService.getAll().subscribe(data => this.categories.set(data));
    }

    loadItems(): void {
        this.itemService.getAll().subscribe(data => this.items.set(data));
    }

    addItem(): void {
        if (!this.newItem.name || !this.newItem.categoryId) return;
        this.itemService.create(this.newItem).subscribe(() => {
            this.loadItems();
            this.newItem = {
                name: '',
                price: 0,
                categoryId: 0,
                sourcePlatform: SourcePlatform.Taobao,
                condition: ItemCondition.New,
            };
        });
    }

    deleteItem(itemId: number): void {
        this.itemService.delete(itemId).subscribe(() => this.loadItems());
        };
        
}

