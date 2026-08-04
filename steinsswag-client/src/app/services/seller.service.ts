import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Seller, CreateSeller, PlacementSlot, CreatePlacementSlot } from '../models/seller.model';
import { environment } from '../../environments/environment';

@Injectable({ providedIn: 'root' })
export class SellerService {
  private readonly baseUrl = `${environment.apiUrl}/sellers`;

  constructor(private http: HttpClient) {}

  getAll(): Observable<Seller[]> {
    return this.http.get<Seller[]>(this.baseUrl);
  }

  getById(id: number): Observable<Seller> {
    return this.http.get<Seller>(`${this.baseUrl}/${id}`);
  }

  create(dto: CreateSeller): Observable<Seller> {
    return this.http.post<Seller>(this.baseUrl, dto);
  }

  delete(id: number): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/${id}`);
  }

  getPlacementSlots(sellerId: number): Observable<PlacementSlot[]> {
    return this.http.get<PlacementSlot[]>(`${this.baseUrl}/${sellerId}/placement-slots`);
  }

  createPlacementSlot(dto: CreatePlacementSlot): Observable<PlacementSlot> {
    return this.http.post<PlacementSlot>(`${this.baseUrl}/placement-slots`, dto);
  }
}
