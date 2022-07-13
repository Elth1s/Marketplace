export enum CatalogActionTypes {
    GET_CATALOG = "GET_CATALOG",
    GET_CATALOG_WITH_PRODUCTS = "GET_CATALOG_WITH_PRODUCTS",
    GET_PARENTS = "GET_PARENTS",
    GET_FILTERS_BY_CATEGORY = "GET_FILTERS_BY_CATEGORY"
}

export interface ICatalogItem {
    name: string,
    image: string,
    urlSlug: string
}

export interface IProductItem {
    name: string,
    image: string,
    price: number,
    statusName: string,
    urlSlug: string,
    discount: number | null
}

export interface IFilterValue {
    id: number,
    value: string,
}

export interface IFilterName {
    id: number,
    name: string,
    unitMeasure: string,
    filterValues: Array<IFilterValue>,
}

export interface CatalogState {
    parents: Array<ICatalogItem>,
    name: string,
    catalogItems: Array<ICatalogItem>,
    products: Array<IProductItem>,
    count: number,
    filterNames: Array<IFilterName>
}

export interface ICatalogWithProducts {
    name: string,
    catalogItems: Array<ICatalogItem>,
    products: Array<IProductItem>,
    count: number
}

export interface GetCatalogAction {
    type: CatalogActionTypes.GET_CATALOG,
    payload: Array<ICatalogItem>
}

export interface GetFilterByCategoryAction {
    type: CatalogActionTypes.GET_FILTERS_BY_CATEGORY,
    payload: Array<IFilterName>
}

export interface GetCatalogWithProductsAction {
    type: CatalogActionTypes.GET_CATALOG_WITH_PRODUCTS,
    payload: ICatalogWithProducts
}

export interface GetParentsAction {
    type: CatalogActionTypes.GET_PARENTS,
    payload: Array<ICatalogItem>
}

export type CatalogAction = GetCatalogAction |
    GetCatalogWithProductsAction |
    GetFilterByCategoryAction |
    GetParentsAction;