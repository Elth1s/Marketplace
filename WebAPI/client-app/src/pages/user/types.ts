export enum ProfileActionTypes {
    GET_PROFILE = "GET_PROFILE",
    GOOGLE_CONNECT_SUCCESS = "GOOGLE_CONNECT_SUCCESS",
    FACEBOOK_CONNECT_SUCCESS = "FACEBOOK_CONNECT_SUCCESS",
}
export enum ConfirmEmailActionTypes {
    CONFIRM_EMAIL = "CONFIRM_EMAIL",
    SEND_CONFIRM_EMAIL = "SEND_CONFIRM_EMAIL",
    IS_EMAIL_CONFIRMED = "IS_EMAIL_CONFIRMED"
}
export enum OrderActionTypes {
    GET_ORDER_PRODUCTS = "GET_ORDER_PRODUCTS",
    GET_ORDER_FOR_USER = "GET_ORDER_FOR_USER"
}
export enum GenderActionTypes {
    GET_GENDERS = "GET_GENDERS"
}

//Profile interface
export interface IProfile {
    firstName: string,
    secondName: string,
    genderId: number | string,
    countryId: number | string,
    cityId: number | string,
    address: string,
    postalCode: string,

    email: string,
    phone: string,

    photo: string,
    isEmailConfirmed: boolean,
    isPhoneConfirmed: boolean,
    isGoogleConnected: boolean,
    isFacebookConnected: boolean,
    hasPassword: boolean
}

export interface IGender {
    id: number,
    name: string
}

//Confirm Email
export interface IConfirmEmail {
    userId: string,
    confirmationCode: string
}

//Order
export interface IOrderCreate {
    consumerFirstName: string,
    consumerSecondName: string,
    consumerPhone: string,
    consumerEmail: string,
    basketItems: Array<IBasketOrderItem>,
    deliveryTypeId: number | string
}

export interface IBasketOrderItem {
    id: number,
    productId: number,
    productName: string,
    productImage: string,
    productPrice: number,
    productDiscount: number,
    productPriceSum: number,
    productUrlSlug: string,
    count: number
}

export interface IDeliveryType {
    id: number,
    name: string,
}

export interface IOrderProducts {
    shopName: string,
    totalPrice: number,
    totalCount: number,
    deliveryTypes: Array<IDeliveryType>
    basketItems: Array<IBasketOrderItem>
}

export interface IProductForUser {
    id: number,
    count: number,
    price: number,
    productId: number,
    productName: string,
    productUrlSlug: string,
    productImage: string,
}

export interface IOrdersForUser {
    id: number,
    consumerFirstName: string,
    consumerSecondName: string,
    consumerPhone: string,
    consumerEmail: string,
    orderStatusName: null,
    deliveryType: string,
    totalPrice: number,
    orderProductsResponse: Array<IProductForUser>
}


//==========State
export interface ProfileState {
    userInfo: IProfile,
    orderProducts: Array<IOrderProducts>
    ordersForUser: Array<IOrdersForUser>
    genders: Array<IGender>
}

//==========Action
//Profile
export interface GetProfileAction {
    type: ProfileActionTypes.GET_PROFILE,
    payload: IProfile
}
export interface GoogleConnectSuccessAction {
    type: ProfileActionTypes.GOOGLE_CONNECT_SUCCESS,
}
export interface FacebookConnectSuccessAction {
    type: ProfileActionTypes.FACEBOOK_CONNECT_SUCCESS,
}

//Confirm Email
export interface ConfirmEmailAction {
    type: ConfirmEmailActionTypes.CONFIRM_EMAIL,
}
export interface SendConfirmEmailAction {
    type: ConfirmEmailActionTypes.SEND_CONFIRM_EMAIL,
}
export interface IsEmailConfirmedAction {
    type: ConfirmEmailActionTypes.IS_EMAIL_CONFIRMED,
    payload: boolean
}

//Order
export interface GetOrderProductsAction {
    type: OrderActionTypes.GET_ORDER_PRODUCTS,
    payload: Array<IOrderProducts>
}

export interface GetOrderForUserAction {
    type: OrderActionTypes.GET_ORDER_FOR_USER,
    payload: Array<IOrdersForUser>
}

export interface GetGendersAction {
    type: GenderActionTypes.GET_GENDERS,
    payload: Array<IGender>
}

export type ProfileAction = GetProfileAction | GoogleConnectSuccessAction | FacebookConnectSuccessAction;
export type EmailConfirmAction = ConfirmEmailAction | SendConfirmEmailAction | IsEmailConfirmedAction;
export type OrderAction = GetOrderProductsAction | GetOrderForUserAction;
export type GenderAction = GetGendersAction;