import { Component, OnInit, signal } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpErrorResponse } from '@angular/common/http';
import { Seller, PlacementSlot, CreatePlacementSlot } from '../../models/seller.model';
import { SellerService } from '../../services/seller.service';
import { extractErrorMessage } from '../../utils/error-utils';
import { DatePipe } from '@angular/common';
@Component({
  selector: 'app-placement-slot-list',
  standalone: true,
  imports: [FormsModule, DatePipe],
  templateUrl: './placement-slot-list.component.html',
})
export class PlacementSlotListComponent implements OnInit {
  sellers = signal<Seller[]>([]);
  slots = signal<PlacementSlot[]>([]);
  loading = signal(false);
  error = signal<string | null>(null);

  selectedSellerId: number = 0;

  newSlot: CreatePlacementSlot = this.emptySlot();

  constructor(private sellerService: SellerService) {}

  ngOnInit(): void {
    this.loading.set(true);
    this.error.set(null);

    this.sellerService.getAll().subscribe({
      next: data => {
        this.sellers.set(data);
        this.loading.set(false);
      },
      error: (err: HttpErrorResponse) => {
        console.error('Failed to load sellers', err);
        this.error.set(extractErrorMessage(err, 'Failed to load sellers. Please try again.'));
        this.loading.set(false);
      }
    });
  }

  onSellerChange(): void {
    if (!this.selectedSellerId) {
      this.slots.set([]);
      return;
    }
    this.loadSlots();
  }

  loadSlots(): void {
    this.loading.set(true);
    this.error.set(null);

    this.sellerService.getPlacementSlots(this.selectedSellerId).subscribe({
      next: data => {
        this.slots.set(data);
        this.loading.set(false);
      },
      error: (err: HttpErrorResponse) => {
        console.error('Failed to load placement slots', err);
        this.error.set(extractErrorMessage(err, 'Failed to load placement slots. Please try again.'));
        this.loading.set(false);
      }
    });
  }

  addSlot(): void {
    if (!this.selectedSellerId || !this.newSlot.startDate) return;

    this.newSlot.sellerId = this.selectedSellerId;

    this.sellerService.createPlacementSlot(this.newSlot).subscribe({
      next: () => {
        this.loadSlots();
        this.newSlot = this.emptySlot();
      },
      error: (err: HttpErrorResponse) => {
        console.error('Failed to add placement slot', err);
        this.error.set(extractErrorMessage(err, 'Failed to add placement slot. Please try again.'));
      }
    });
  }

  private emptySlot(): CreatePlacementSlot {
    return {
      sellerId: this.selectedSellerId,
      position: 1,
      price: 0,
      startDate: '',
      endDate: undefined,
    };
  }
}