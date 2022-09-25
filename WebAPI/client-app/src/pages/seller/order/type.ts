export enum OrderActionTypes {
    GET_ORDERS = "GET_ORDERS",
    SEARCH_ORDERS = "SEARCH_ORDERS",
    GET_BY_ID = "GET_BY_ID"
}

export interface IOrderInfo {
    id: number,
    orderStatusName: string,
    consumerFirstName: string,
    consumerSecondName: string,
    consumerPhone: string,
    city: string,
    department: string,
    totalPrice: number,
    deliveryType: string,
}


export interface IOrderProduct {
    id: number,
    count: number,
    price: number,
    productId: number,
    productName: string,
    productUrlSlug: string,
    productImage: string,
}

export interface IOrderFullInfo {
    id: number,
    consumerFirstName: string,
    consumerSecondName: string,
    consumerPhone: string,
    consumerEmail: string,
    orderStatusId: number,
    orderStatusName: string,
    trackingNumber: string,
    deliveryType: string,
    city: string,
    department: string,
    totalPrice: number,
    date: string,
    canUpdate: boolean,
    orderProductsResponse: Array<IOrderProduct>
}

export interface IOrderUpdate {
    orderStatusId: number,
    trackingNumber: string
}

export interface ISearchOrders {
    count: number,
    values: Array<IOrderInfo>
}


export interface OrderState {
    selectedOrder: IOrderFullInfo,
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

export interface GetByIdAction {
    type: OrderActionTypes.GET_BY_ID,
    payload: IOrderFullInfo
}

export type OrderAction = GetOrdersAction | SearchOrdersAction | GetByIdAction;