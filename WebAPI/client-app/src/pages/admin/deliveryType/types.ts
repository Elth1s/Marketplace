export enum DeliveryTypeActionTypes {
    GET_DELIVERY_TYPES = "GET_DELIVERY_TYPES",
    SEARCH_DELIVERY_TYPES = "SEARCH_DELIVERY_TYPES",
    GET_DELIVERY_TYPES_FOR_SELECT = "GET_DELIVERY_TYPES_FOR_SELECT",
    GET_BY_ID_DELIVERY_TYPES = "GET_BY_ID_DELIVERY_TYPES",
    CREATE_DELIVERY_TYPE = "CREATE_DELIVERY_TYPE",
    UPDATE_DELIVERY_TYPE = "UPDATE_DELIVERY_TYPE",
}

export interface IDeliveryType {
    englishName: string,
    ukrainianName: string,
    darkIcon: string,
    lightIcon: string,
}

export interface IDeliveryTypeInfo {
    id: number,
    name: string,
    urlSlug: string,
    darkIcon: string,
    lightIcon: string,
}

export interface IDeliveryTypeForSelect {
    id: number
    name: string,
}

export interface ISearchDeliveryTypes {
    count: number,
    values: Array<IDeliveryTypeInfo>
}


export interface DeliveryTypeState {
    selectedDeliveryType: IDeliveryType,
    deliveryTypes: Array<IDeliveryTypeInfo>,
    deliveryTypesForSelect: Array<IDeliveryTypeForSelect>,
    count: number
}



export interface GetDeliveryTypesAction {
    type: DeliveryTypeActionTypes.GET_DELIVERY_TYPES,
    payload: Array<IDeliveryTypeInfo>
}

export interface SearchDeliveryTypesAction {
    type: DeliveryTypeActionTypes.SEARCH_DELIVERY_TYPES,
    payload: ISearchDeliveryTypes
}


export interface GetDeliveryTypesForSelectAction {
    type: DeliveryTypeActionTypes.GET_DELIVERY_TYPES_FOR_SELECT,
    payload: Array<IDeliveryTypeForSelect>
}

export interface GetByIdDeliveryTypeAction {
    type: DeliveryTypeActionTypes.GET_BY_ID_DELIVERY_TYPES,
    payload: IDeliveryType
}

export interface CreateDeliveryTypeAction {
    type: DeliveryTypeActionTypes.CREATE_DELIVERY_TYPE,
    payload: IDeliveryType
}

export interface UpdateDeliveryTypeAction {
    type: DeliveryTypeActionTypes.UPDATE_DELIVERY_TYPE,
    payload: IDeliveryType
}


export type DeliveryTypeAction = GetDeliveryTypesAction |
    GetDeliveryTypesForSelectAction |
    SearchDeliveryTypesAction |
    GetByIdDeliveryTypeAction |
    CreateDeliveryTypeAction |
    UpdateDeliveryTypeAction;