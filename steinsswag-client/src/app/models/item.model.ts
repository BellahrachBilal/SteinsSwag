export enum SourcePlatform {
    Taobao = 'Taobao',
    Weidian = 'Weidian',
    Xianyu = 'Xianyu',
    MercariJp = 'MercariJp',
    Vinted = 'Vinted',
    Other = 'Other'
}

export enum ItemCondition {
   New = 'New',
  LikeNew = 'LikeNew',
  Good = 'Good',
  Fair = 'Fair',
  Used = 'Used'
}

export enum ItemStatus {
    Available = 'Available',
    Reserved = 'Reserved',
    Sold = 'Sold',
    Archived = 'Archived'
}

export interface Item {
    id: number;
    name: string;
    description?: string;
    brand?: string;
    price: number;
    imageUrl?: string;
    categoryId: number;
    categoryName?: string;
    sellerId?: number;
    sellerName?: string;
    sourcePlatform: SourcePlatform;
    condition: ItemCondition;
    status: ItemStatus;
    createdAt: string;
}

export interface CreateItem {
  name: string;
  description?: string;
  brand?: string;
  price: number;
  imageUrl?: string;
  categoryId: number;
  sellerId?: number;
  sourcePlatform: SourcePlatform;
  condition: ItemCondition;
}

export interface UpdateItem extends CreateItem {
  status: ItemStatus;
}