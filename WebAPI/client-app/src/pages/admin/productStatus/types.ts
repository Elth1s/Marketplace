export enum ProductStatusActionTypes {
    GET_PRODUCT_STATUSES = "GET_PRODUCT_STATUSES",
    GET_BY_ID_PRODUCT_STATUS = "GET_BY_ID_PRODUCT_STATUS",
    CREATE_PRODUCT_STATUS = "CREATE_PRODUCT_STATUS",
    UPDATE_PRODUCT_STATUS = "UPDATE_PRODUCT_STATUS",
    SEARCH_PRODUCT_STATUSES = "SEARCH_PRODUCT_STATUSES"
}

export interface IProductStatus {
    name: string,
}

export interface IProductStatusInfo {
    id: number,
    name: string,
}

export interface ISearchProductStatuses {
    count: number,
    values: Array<IProductStatusInfo>
}

export interface ProductStatusState {
    selectedProductStatus: IProductStatus,
    count: number,
    productStatuses: Array<IProductStatusInfo>
}


export interface GetProductStatusesAction {
    type: ProductStatusActionTypes.GET_PRODUCT_STATUSES,
    payload: Array<IProductStatusInfo>
}

export interface SearchProductStatusesAction {
    type: ProductStatusActionTypes.SEARCH_PRODUCT_STATUSES,
    payload: ISearchProductStatuses
}

export interface GetByIdProductStatusAction {
    type: ProductStatusActionTypes.GET_BY_ID_PRODUCT_STATUS,
    payload: IProductStatus
}

export interface CreateProductStatusAction {
    type: ProductStatusActionTypes.CREATE_PRODUCT_STATUS,
    payload: IProductStatus
}

export interface UpdateProductStatusAction {
    type: ProductStatusActionTypes.UPDATE_PRODUCT_STATUS,
    payload: IProductStatus
}


export type ProductStatusAction = GetProductStatusesAction |
    SearchProductStatusesAction |
    GetByIdProductStatusAction |
    CreateProductStatusAction |
    UpdateProductStatusAction;