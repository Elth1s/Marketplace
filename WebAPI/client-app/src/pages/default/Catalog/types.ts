export enum CatalogActionTypes {
    GET_CATALOG = "GET_CATALOG",
    GET_FULL_CATALOG = "GET_FULL_CATALOG",
    GET_CATALOG_WITH_PRODUCTS = "GET_CATALOG_WITH_PRODUCTS",
    GET_MORE_PRODUCTS = "GET_MORE_PRODUCTS",
    GET_PARENTS = "GET_PARENTS",
    GET_FILTERS_BY_CATEGORY = "GET_FILTERS_BY_CATEGORY",
    RESET_FILTERS = "RESET_FILTERS",
    UPDATE_SEARCH = "UPDATE_SEARCH",
    SEARCH_PRODUCTS = "SEARCH_PRODUCTS",
    GET_CATEGORIES_FOR_SEARCH = "GET_CATEGORIES_FOR_SEARCH",
    GET_NOVELTIES = "GET_NOVELTIES",
    CHANGE_IS_SELECTED_PRODUCTS = "CHANGE_IS_SELECTED_PRODUCTS",
    CHANGE_IS_SELECTED_USER_PRODUCTS = "CHANGE_IS_SELECTED_USER_PRODUCTS",
    CHANGE_IS_IN_CART_USER_PRODUCTS = "CHANGE_IS_IN_CART_USER_PRODUCTS",
    GET_SIMILAR_PRODUCTS = "GET_SIMILAR_PRODUCTS",

    GET_SELECTED_PRODUCTS = "GET_SELECTED_PRODUCTS",
    GET_REVIEWED_PRODUCTS = "GET_REVIEWED_PRODUCTS",

    GET_SALE_CATEGORIES = "GET_SALE_CATEGORIES",
    GET_SALE_PRODUCTS = "GET_SALE_PRODUCTS",
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
    id: number,
    isSelected: boolean,
    name: string,
    image: string,
    price: number,
    statusName: string,
    urlSlug: string,
    discount: number | null
}

export interface IUserProductItem {
    id: number,
    isSelected: boolean,
    name: string,
    image: string,
    price: number,
    statusName: string,
    urlSlug: string,
    discount: number | null,
    isInBasket: boolean
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
    searchCatalog: Array<IFullCatalogItem>,
    userProducts: Array<IUserProductItem>
}

export interface ICatalogWithProducts {
    name: string,
    catalogItems: Array<ICatalogItem>,
    products: Array<IProductItem>,
    countProducts: number
}

export interface IProductResponse {
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

export interface ResetFiltersAction {
    type: CatalogActionTypes.RESET_FILTERS
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
    payload: IProductResponse
}

export interface GetCategoriesForSearchAction {
    type: CatalogActionTypes.GET_CATEGORIES_FOR_SEARCH,
    payload: Array<IFullCatalogItem>
}

export interface GetNoveltiesAction {
    type: CatalogActionTypes.GET_NOVELTIES,
    payload: IProductResponse
}

export interface ChangeIsSelectedProductsAction {
    type: CatalogActionTypes.CHANGE_IS_SELECTED_PRODUCTS,
    payload: string
}

export interface ChangeIsSelectedUserProductsAction {
    type: CatalogActionTypes.CHANGE_IS_SELECTED_USER_PRODUCTS,
    payload: string
}

export interface ChangeIsInCartUserProductsAction {
    type: CatalogActionTypes.CHANGE_IS_IN_CART_USER_PRODUCTS,
    payload: string
}


export interface GetSimilarProductsAction {
    type: CatalogActionTypes.GET_SIMILAR_PRODUCTS,
    payload: Array<IProductItem>
}

export interface GetSelectedProductsAction {
    type: CatalogActionTypes.GET_SELECTED_PRODUCTS,
    payload: Array<IUserProductItem>
}

export interface GetReviewedProductsAction {
    type: CatalogActionTypes.GET_REVIEWED_PRODUCTS,
    payload: Array<IUserProductItem>
}

export interface GetSaleProductsAction {
    type: CatalogActionTypes.GET_SALE_PRODUCTS,
    payload: IProductResponse
}

export interface GetSaleCategoriesAction {
    type: CatalogActionTypes.GET_SALE_CATEGORIES,
    payload: Array<IFullCatalogItem>
}



export type CatalogAction = GetCatalogAction |
    GetFullCatalogAction |
    GetCatalogWithProductsAction |
    GetMoreProductsAction |
    GetFilterByCategoryAction |
    ResetFiltersAction |
    GetParentsAction |
    UpdateSearchAction |
    SearchProductsAction |
    GetCategoriesForSearchAction |
    GetNoveltiesAction |
    ChangeIsSelectedProductsAction |
    GetSimilarProductsAction |
    GetReviewedProductsAction |
    GetSelectedProductsAction |
    ChangeIsSelectedUserProductsAction |
    ChangeIsInCartUserProductsAction |
    GetSaleProductsAction |
    GetSaleCategoriesAction;