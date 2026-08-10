import { Component, OnInit, signal } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpErrorResponse } from '@angular/common/http';
import { Seller, CreateSeller, PricingModel } from '../../models/seller.model';
import { SellerService } from '../../services/seller.service';
import { extractErrorMessage } from '../../utils/error-utils';

@Component({
  selector: 'app-seller-list',
  standalone: true,
  imports: [FormsModule],
  templateUrl: './seller-list.component.html',
})
export class SellerListComponent implements OnInit {
  sellers = signal<Seller[]>([]);
  loading = signal(false);
  error = signal<string | null>(null);

  pricingModels = Object.values(PricingModel);

  newSeller: CreateSeller = this.emptySeller();

  constructor(private sellerService: SellerService) {}

  ngOnInit(): void {
    this.loadSellers();
  }

  loadSellers(): void {
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

  addSeller(): void {
    if (!this.newSeller.name) return;

    this.sellerService.create(this.newSeller).subscribe({
      next: () => {
        this.loadSellers();
        this.newSeller = this.emptySeller();
      },
      error: (err: HttpErrorResponse) => {
        console.error('Failed to add seller', err);
        this.error.set(extractErrorMessage(err, 'Failed to add seller. Please try again.'));
      }
    });
  }

  deleteSeller(sellerId: number, sellerName: string): void {
    this.sellerService.delete(sellerId).subscribe({
      next: () => this.loadSellers(),
      error: (err: HttpErrorResponse) => {
        console.error('Failed to delete seller', err);
        this.error.set(`Could not delete "${sellerName}" — it may have already been removed.`);
      }
    });
  }

  private emptySeller(): CreateSeller {
    return {
      name: '',
      contactHandle: '',
      pricingModel: PricingModel.FixedRate,
      notes: '',
    };
  }
}