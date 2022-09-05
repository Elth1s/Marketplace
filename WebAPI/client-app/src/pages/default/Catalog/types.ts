export enum CatalogActionTypes {
    GET_CATALOG = "GET_CATALOG",
    GET_FULL_CATALOG = "GET_FULL_CATALOG",
    GET_CATALOG_WITH_PRODUCTS = "GET_CATALOG_WITH_PRODUCTS",
    GET_MORE_PRODUCTS = "GET_MORE_PRODUCTS",
    GET_PARENTS = "GET_PARENTS",
    GET_FILTERS_BY_CATEGORY = "GET_FILTERS_BY_CATEGORY",
    UPDATE_SEARCH = "UPDATE_SEARCH",
    SEARCH_PRODUCTS = "SEARCH_PRODUCTS",
    GET_CATEGORIES_FOR_SEARCH = "GET_CATEGORIES_FOR_SEARCH"
}

export interface ICatalogItem {
    name: string,
    image: string,
    urlSlug: string
}

export interface IFullCatalogItem {
    id: number,
    name: string,
    lightIcon: string,
    darkIcon: string,
    activeIcon: string,
    urlSlug: string,
    countProducts: number,
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
    filterNames: Array<IFilterName>,
    searchField: string,
    searchCatalog: Array<IFullCatalogItem>
}

export interface ICatalogWithProducts {
    name: string,
    catalogItems: Array<ICatalogItem>,
    products: Array<IProductItem>,
    countProducts: number
}

export interface ISearchProducts {
    count: number,
    values: Array<IProductItem>
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

export interface UpdateSearchAction {
    type: CatalogActionTypes.UPDATE_SEARCH,
    payload: string
}

export interface SearchProductsAction {
    type: CatalogActionTypes.SEARCH_PRODUCTS,
    payload: ISearchProducts
}

export interface GetCategoriesForSearchAction {
    type: CatalogActionTypes.GET_CATEGORIES_FOR_SEARCH,
    payload: Array<IFullCatalogItem>
}


export type CatalogAction = GetCatalogAction |
    GetFullCatalogAction |
    GetCatalogWithProductsAction |
    GetMoreProductsAction |
    GetFilterByCategoryAction |
    GetParentsAction |
    UpdateSearchAction |
    SearchProductsAction |
    GetCategoriesForSearchAction;