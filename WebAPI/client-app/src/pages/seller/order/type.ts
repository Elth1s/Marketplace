export enum OrderActionTypes {
    GET_ORDERS = "GET_ORDERS",
    SEARCH_ORDERS = "SEARCH_ORDERS",
}

export interface IOrderInfo {
    id: number,
    consumerFirstName: string,
    consumerSecondName: string,
    consumerPhone: string,
    consumerEmail: string,
    orderStatusName: boolean,
    deliveryType: string,
    orderProductsResponse: string,
}

export interface ISearchOrders {
    count: number,
    values: Array<IOrderInfo>
}


export interface OrderState {
    count: number,
    orders: Array<IOrderInfo>
}


export interface GetOrdersAction {
    type: OrderActionTypes.GET_ORDERS,
    payload: Array<IOrderInfo>
}

export interface SearchOrdersAction {
    type: OrderActionTypes.SEARCH_ORDERS,
    payload: ISearchOrders
}

export type OrderAction = GetOrdersAction | SearchOrdersAction;