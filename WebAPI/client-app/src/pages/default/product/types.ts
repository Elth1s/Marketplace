export enum ProductActionTypes {
    GET_PRODUCT_BY_URLSLUG = "GET_PRODUCT_BY_URLSLUG",
    UPDATE_SELECTED_PRODUCT = "UPDATE_SELECTED_PRODUCT",
}

export interface IParentCategoryItem {
    name: string,
    image: string,
    urlSlug: string
}

export interface IFilterItem {
    value: string,
    filterName: string,
    unitMeasure: string
}

export interface IProductItem {
    isInBasket: boolean,
    name: string,
    shopName: string,
    productStatus: string,
    images: Array<string>,
    price: number,
    filters: Array<IFilterItem>
}

export interface ProductState {
    parents: Array<IParentCategoryItem>,
    product: IProductItem,
}

export interface IProductWithParents {
    product: IProductItem,
    parents: Array<IParentCategoryItem>,
}

export interface GetProductByUrlSlugAction {
    type: ProductActionTypes.GET_PRODUCT_BY_URLSLUG,
    payload: IProductWithParents
}

export interface UpdateSelectedProductAction {
    type: ProductActionTypes.UPDATE_SELECTED_PRODUCT,
}

export type ProductAction = GetProductByUrlSlugAction |
    UpdateSelectedProductAction;