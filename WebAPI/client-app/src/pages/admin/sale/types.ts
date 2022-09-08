export enum SaleActionTypes {
    GET_SALES = "GET_SALES",
    SEARCH_SALES = "SEARCH_SALES",
    GET_BY_ID_SALE = "GET_BY_ID_SALE",
    CREATE_SALE = "CREATE_SALE",
    UPDATE_SALE = "UPDATE_SALE",
}

export interface ISale {
    name: string,
    ukrainianHorizontalImage: string,
    ukrainianVerticalImage: string,
    englishHorizontalImage: string,
    englishVerticalImage: string,
    discountMin: number,
    discountMax: number,
    dateStart: Date | string,
    dateEnd: Date | string
}

export interface ISaleInfo {
    id: number,
    name: string,
    horizontalImage: string,
    verticalImage: string,
    discountMin: number,
    discountMax: number,
    dateStart: Date | string,
    dateEnd: Date | string
}

export interface ISearchSales {
    count: number,
    values: Array<ISaleInfo>
}

export interface SaleState {
    selectedSale: ISale,
    sales: Array<ISaleInfo>,
    count: number
}

export interface GetSalesAction {
    type: SaleActionTypes.GET_SALES,
    payload: Array<ISaleInfo>
}

export interface SearchSalesAction {
    type: SaleActionTypes.SEARCH_SALES,
    payload: ISearchSales
}

export interface GetByIdSaleAction {
    type: SaleActionTypes.GET_BY_ID_SALE,
    payload: ISale
}

export interface CreateSaleAction {
    type: SaleActionTypes.CREATE_SALE,
    payload: ISale
}

export interface UpdateSaleAction {
    type: SaleActionTypes.UPDATE_SALE,
    payload: ISale
}


export type SaleAction = GetSalesAction |
    SearchSalesAction |
    GetByIdSaleAction |
    CreateSaleAction |
    UpdateSaleAction;