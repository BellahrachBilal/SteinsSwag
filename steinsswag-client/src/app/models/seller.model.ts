export enum PricingModel {
  FixedRate = 'FixedRate',
  Commission = 'Commission'
}

export interface Seller {
  id: number;
  name: string;
  contactHandle?: string;
  pricingModel: PricingModel;
  notes?: string;
  createdAt: string;
  itemCount: number;
}

export interface CreateSeller {
  name: string;
  contactHandle?: string;
  pricingModel: PricingModel;
  notes?: string;
}

export interface PlacementSlot {
  id: number;
  sellerId: number;
  sellerName: string;
  position: number;
  price: number;
  startDate: string;
  endDate?: string;
  isActive: boolean;
}

export interface CreatePlacementSlot {
  sellerId: number;
  position: number;
  price: number;
  startDate: string;
  endDate?: string;
}
