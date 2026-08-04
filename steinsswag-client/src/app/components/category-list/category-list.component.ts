import {Component, OnInit, signal} from '@angular/core';
import {CommonModule} from '@angular/common';
import {FormsModule} from '@angular/forms';
import {Category, CreateCategory} from '../../models/category.model';
import {CategoryService} from '../../services/category.service';

@Component({
    selector : 'app-category-list',
    standalone : true,
    imports : [CommonModule, FormsModule],
    templateUrl : './category-list.component.html',
})

export class CategoryListComponent implements OnInit {
    categories = signal<Category[]>([]);

    newCategory: CreateCategory = {
        name: '',
    };

    constructor(private categoryService: CategoryService) { }

    ngOnInit(): void {
        this.loadCategories();
    }

    loadCategories(): void {
        this.categoryService.getAll().subscribe(data => this.categories.set(data));
    }

    addCategory(): void {
        if (!this.newCategory.name) return; 

        this.categoryService.create(this.newCategory).subscribe(() => {
            this.loadCategories();
            this.newCategory = { name: '' };
        });
    }

}