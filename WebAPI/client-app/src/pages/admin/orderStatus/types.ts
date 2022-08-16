export enum OrderStatusActionTypes {
    GET_ORDER_STATUSES = "GET_ORDER_STATUSES",
    GET_BY_ID_ORDER_STATUS = "GET_BY_ID_ORDER_STATUS",
    CREATE_ORDER_STATUS = "CREATE_ORDER_STATUS",
    UPDATE_ORDER_STATUS = "UPDATE_ORDER_STATUS",
    SEARCH_ORDER_STATUSES = "SEARCH_ORDER_STATUSES"
}

export interface IOrderStatus {
    englishName: string,
    ukrainianName: string,
}

export interface IOrderStatusInfo {
    id: number,
    name: string,
}

export interface ISearchOrderStatuses {
    count: number,
    values: Array<IOrderStatusInfo>
}

export interface OrderStatusState {
    selectedOrderStatus: IOrderStatus,
    count: number,
    orderStatuses: Array<IOrderStatusInfo>
}


export interface GetOrderStatusesAction {
    type: OrderStatusActionTypes.GET_ORDER_STATUSES,
    payload: Array<IOrderStatusInfo>
}

export interface SearchOrderStatusesAction {
    type: OrderStatusActionTypes.SEARCH_ORDER_STATUSES,
    payload: ISearchOrderStatuses
}

export interface GetByIdOrderStatusAction {
    type: OrderStatusActionTypes.GET_BY_ID_ORDER_STATUS,
    payload: IOrderStatus
}

export interface CreateOrderStatusAction {
    type: OrderStatusActionTypes.CREATE_ORDER_STATUS,
    payload: IOrderStatus
}

export interface UpdateOrderStatusAction {
    type: OrderStatusActionTypes.UPDATE_ORDER_STATUS,
    payload: IOrderStatus
}


export type OrderStatusAction = GetOrderStatusesAction |
    SearchOrderStatusesAction |
    GetByIdOrderStatusAction |
    CreateOrderStatusAction |
    UpdateOrderStatusAction;