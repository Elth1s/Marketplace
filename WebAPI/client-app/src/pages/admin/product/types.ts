export enum ProductActionTypes {
    GET_PRODUCTS = "GET_PRODUCTS",
    SEARCH_PRODUCTS = "SEARCH_PRODUCTS"
}

export interface IProductInfo {
    id: number,
    name: string,
    image: string,
    price: number,
    count: number,
    statusName: string,
    categoryName: string
}

export interface ISearchProducts {
    count: number,
    values: Array<IProductInfo>
}

export interface ProductState {
    count: number,
    products: Array<IProductInfo>
}


export interface GetProductsAction {
    type: ProductActionTypes.GET_PRODUCTS,
    payload: Array<IProductInfo>
}

export interface SearchProductsAction {
    type: ProductActionTypes.SEARCH_PRODUCTS,
    payload: ISearchProducts
}


export type ProductAction = GetProductsAction |
    SearchProductsAction