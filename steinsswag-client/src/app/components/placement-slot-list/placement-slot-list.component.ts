import {Component, OnInit, signal} from '@angular/core';
import {CommonModule} from '@angular/common';
import {FormsModule} from '@angular/forms';
import {Seller, PlacementSlot, CreatePlacementSlot} from '../../models/seller.model';
import {SellerService} from '../../services/seller.service';

@Component({
    selector : 'app-placement-slot-list',
    standalone : true,
    imports : [CommonModule, FormsModule],
    templateUrl : './placement-slot-list-component.html',
})

export class PlacementSlotListComponent implements OnInit {
    sellers = signal<Seller[]>([]);
    slots = signal <PlacementSlot[]>([]);

    selectedSellerId: number = 0;

    newSlot: CreatePlacementSlot = {
        sellerId: 0,
        position: 1,
        price: 0,
        startDate: '',
        endDate: undefined,
    };

    constructor(private sellerService: SellerService) { }

    ngOnInit(): void {
        this.sellerService.getAll().subscribe(data => this.sellers.set(data));
    }
    onSellerChange(): void {
        if (!this.selectedSellerId) {
            this.slots.set([]);
            return;
        }
        this.loadSlots();
    }

    loadSlots(): void {
        this.sellerService.getPlacementSlots(this.selectedSellerId).subscribe(data => this.slots.set(data));
    }

    addSlot(): void {
        if (!this.selectedSellerId || !this.newSlot.startDate) return;

        this.newSlot.sellerId = this.selectedSellerId;

        this.sellerService.createPlacementSlot(this.newSlot).subscribe(() => {
            this.loadSlots();
            this.newSlot = {
                sellerId: this.selectedSellerId,
                position: 1,
                price: 0,
                startDate: '',
                endDate: undefined,
            };
        });
    }
}


