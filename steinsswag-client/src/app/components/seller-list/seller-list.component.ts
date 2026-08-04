import {Component, OnInit, signal} from '@angular/core';
import {CommonModule} from '@angular/common';
import {FormsModule} from '@angular/forms';
import {Seller, CreateSeller, PricingModel} from '../../models/seller.model';
import {SellerService} from '../../services/seller.service';


@Component({
  selector: 'app-seller-list',
  standalone: true,
    imports: [CommonModule, FormsModule],
    templateUrl: './seller-list.component.html',
})

export class SellerListComponent implements OnInit {
    sellers = signal<Seller[]>([]);
    pricingModels = Object.values(PricingModel);
    
    newSeller: CreateSeller = {
        name: '',
        contactHandle: '',
        pricingModel: PricingModel.FixedRate,
        notes: '',
    };

    constructor(private sellerService: SellerService) { }

    ngOnInit(): void {
        this.loadSellers();
    }

    loadSellers(): void {
        this.sellerService.getAll().subscribe(data => this.sellers.set(data));
    }

    addSeller(): void {
        if (!this.newSeller.name) return;
        this.sellerService.create(this.newSeller).subscribe(() => {
            this.loadSellers();
            this.resetNewSeller();
        });
    }

    resetNewSeller(): void {
        this.newSeller = {
            name: '',
            contactHandle: '',
            pricingModel: PricingModel.FixedRate,
            notes: '',
        };
    }

    deleteSeller(sellerId: number): void {
        this.sellerService.delete(sellerId).subscribe(() => this.loadSellers());
    }

}

