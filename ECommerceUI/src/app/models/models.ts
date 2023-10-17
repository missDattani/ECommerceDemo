export interface Category
{
    categoryId:number;
    category:string;
    subCategory:string;
}

export interface SuggestedProduct
{
    bannerimage : string;
    category: Category;
}

export interface NavigationItems
{
    category:string;
    subcategories:string[];
}

export interface Offers{
    id:number;
    title:string,
    discount:number;
}

export interface Product{
    productId:number;
    title:string;
    description:string;
    categoryId:number;
    category:string;
    subCategory:string;
    price:number;
    offerId:number;
    offerTitle:string;
    discount:number;
    quantity:number;
    imageName:string;
}

export interface User{
    userId:number;
    firstName: string,
    lastName: string,
    email: string,
    address: string,
    mobile: string,
    password: string,
    createdAt: string,
    modifiedAt: string
}

export interface ReviewData{
    reviewId:number,
    userId:number,
    productId:number,
    review:string,
    createdAt:string,
    firstName:string,
    lastName:string,
    
}

export interface Cart{
    cartId:number,
    user:User | null,
    cartItems : CartItems[],
    ordered:boolean,
    orderedOn:string
}

export interface CartItems{
    cartItemId:number,
    productId:number;
    title:string;
    description:string;
    categoryId:number;
    category:string;
    subCategory:string;
    price:number;
    offerId:number;
    offerTitle:string;
    discount:number;
    quantity:number;
    imageName:string;     
}

export interface Payment{
    id:number,
    userId:number,
    paymentMethodId:number,
    type:string,
    provider:string,
    avilable:boolean,
    reason:string,
    totalAmount:number,
    shippingCharges:number,
    amountReduced:number,
    amountPaid:number,
    createdAt:string
}

export interface PaymentMethod{
    paymentMethodId:number,
    type:string,
    provider:string,
    avilable:boolean,
    reason:string
}

export interface Orders{
    id:number,
    userId:number,
    cartId:number,
    paymentId:number,
    createdAt:string
}