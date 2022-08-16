export enum CatalogActionTypes {
    GET_CATALOG = "GET_CATALOG",
    GET_FULL_CATALOG = "GET_FULL_CATALOG",
    GET_CATALOG_WITH_PRODUCTS = "GET_CATALOG_WITH_PRODUCTS",
    GET_MORE_PRODUCTS = "GET_MORE_PRODUCTS",
    GET_PARENTS = "GET_PARENTS",
    GET_FILTERS_BY_CATEGORY = "GET_FILTERS_BY_CATEGORY"
}

export interface ICatalogItem {
    name: string,
    image: string,
    urlSlug: string
}

export interface IFullCatalogItem {
    name: string,
    icon: string,
    urlSlug: string,
    children: Array<IFullCatalogItem>
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
    fullCatalogItems: Array<IFullCatalogItem>,
    products: Array<IProductItem>,
    countProducts: number,
    filterNames: Array<IFilterName>
}

export interface ICatalogWithProducts {
    name: string,
    catalogItems: Array<ICatalogItem>,
    products: Array<IProductItem>,
    countProducts: number
}

export interface GetCatalogAction {
    type: CatalogActionTypes.GET_CATALOG,
    payload: Array<ICatalogItem>
}

export interface GetFullCatalogAction {
    type: CatalogActionTypes.GET_FULL_CATALOG,
    payload: Array<IFullCatalogItem>
}

export interface GetFilterByCategoryAction {
    type: CatalogActionTypes.GET_FILTERS_BY_CATEGORY,
    payload: Array<IFilterName>
}

export interface GetCatalogWithProductsAction {
    type: CatalogActionTypes.GET_CATALOG_WITH_PRODUCTS,
    payload: ICatalogWithProducts
}

export interface GetMoreProductsAction {
    type: CatalogActionTypes.GET_MORE_PRODUCTS,
    payload: IProductItem[]
}

export interface GetParentsAction {
    type: CatalogActionTypes.GET_PARENTS,
    payload: Array<ICatalogItem>
}

export type CatalogAction = GetCatalogAction |
    GetFullCatalogAction |
    GetCatalogWithProductsAction |
    GetMoreProductsAction |
    GetFilterByCategoryAction |
    GetParentsAction;