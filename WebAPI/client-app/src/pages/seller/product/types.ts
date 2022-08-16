export enum ProductActionTypes {
    GET_PRODUCTS = "GET_PRODUCTS",
    SEARCH_PRODUCTS = "SEARCH_PRODUCTS",
    GET_PRODUCT_STATUSES = "GET_PRODUCT_STATUSES",
    GET_CATEGORIES_WITHOUT_CHILDREN = "GET_CATEGORIES_WITHOUT_CHILDREN",
    GET_FILTERS_BY_CATEGORY_ID = "GET_FILTERS_BY_CATEGORY_ID",
}

export interface ICategory {
    id: number,
    name: string
}

export interface IProductStatus {
    id: number,
    name: string
}

export interface IProductImage {
    id: number,
    name: string,
    priority: number
}

export interface IFilterRequest {
    nameId: number,
    valueId: number,
    customValue: number | null
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

export interface IProductCreate {
    name: string,
    description: string,
    price: number,
    count: number,
    statusId: number,
    categoryId: number,
    images: Array<IProductImage>,
    filtersValue: Array<IFilterRequest>
}

export interface IFilterGroupSeller {
    id: number,
    name: string,
    filterNames: Array<IFilterNameSeller>
}

export interface IFilterNameSeller {
    id: number,
    name: string,
    unitMeasure: string,
    filterValues: Array<IFilterValueSeller>
}

export interface IFilterValueSeller {
    id: number,
    value: string,
    min: number,
    max: number,
}

export interface ISearchProducts {
    count: number,
    values: Array<IProductInfo>
}

export interface ProductState {
    count: number,
    products: Array<IProductInfo>,
    productStatuses: Array<IProductStatus>,
    categories: Array<ICategory>,
    filters: Array<IFilterGroupSeller>
}


export interface GetProductsAction {
    type: ProductActionTypes.GET_PRODUCTS,
    payload: Array<IProductInfo>
}

export interface SearchProductsAction {
    type: ProductActionTypes.SEARCH_PRODUCTS,
    payload: ISearchProducts
}

export interface GetProductStatusesAction {
    type: ProductActionTypes.GET_PRODUCT_STATUSES,
    payload: Array<IProductStatus>
}

export interface GetCategoriesWithoutChildrenAction {
    type: ProductActionTypes.GET_CATEGORIES_WITHOUT_CHILDREN,
    payload: Array<ICategory>
}

export interface GetFiltersByCategoryIdAction {
    type: ProductActionTypes.GET_FILTERS_BY_CATEGORY_ID,
    payload: Array<IFilterGroupSeller>
}


export type ProductAction = GetProductsAction |
    GetProductStatusesAction |
    GetCategoriesWithoutChildrenAction |
    GetFiltersByCategoryIdAction |
    SearchProductsAction;