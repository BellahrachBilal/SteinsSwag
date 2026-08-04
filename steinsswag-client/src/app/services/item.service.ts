import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Item, CreateItem, UpdateItem, ItemStatus } from '../models/item.model';
import { environment } from '../../environments/environment';


@Injectable({
  providedIn: 'root'
})
export class ItemService {
    private readonly baseUrl = `${environment.apiUrl}/items`;

    constructor(private http: HttpClient) {}

    getAll(categoryId?: number, status?: ItemStatus): Observable<Item[]> {
        let params =new HttpParams();
        if (categoryId) params = params.set('categoryId', categoryId);
        if (status) params = params.set('status', status);
        return this.http.get<Item[]>(this.baseUrl, { params });
    }

    getById(id: number): Observable<Item> {
        return this.http.get<Item>(`${this.baseUrl}/${id}`);
    }

    create(dto: CreateItem): Observable<Item> {
        return this.http.post<Item>(this.baseUrl, dto);
    }

    update(id: number, dto: UpdateItem): Observable<void> {
        return this.http.put<void>(`${this.baseUrl}/${id}`, dto);
    }
    delete(id: number): Observable<void> {
        return this.http.delete<void>(`${this.baseUrl}/${id}`);
    }
}
