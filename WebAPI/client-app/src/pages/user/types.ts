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
    GET_ORDER_PRODUCTS = "GET_ORDER_PRODUCTS"
}


export interface IProfile {
    firstName: string,
    secondName: string,

    gender: string,
    birthDate: Date | null,
    languageOfCommunication: number,
    region: number,
    address: string,
    city: string,
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


export interface ProfileState {
    userInfo: IProfile,
    orderProducts: Array<IOrderProducts>

}

// Create order


export interface IOrderItem {
    products: Array<IBasketOrderItem>
    deliveryType: string
}

export interface IOrderCreate {
    consumerFirstName: string,
    consumerSecondName: string,
    consumerPhone: string,
    consumerEmail: string,
    orders: Array<IOrderItem>
}

export interface IBasketOrderItem {
    productName: string,
    productImage: string,
    productPrice: number,
    productUrlSlug: string,
    count: number
}

export interface IOrderProducts {
    shopName: string,
    totalPrice: number,
    basketItems: Array<IBasketOrderItem>
}


//Confirm Email
export interface IConfirmEmail {
    userId: string,
    confirmationCode: string
}
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

export interface GetOrderProductsAction {
    type: OrderActionTypes.GET_ORDER_PRODUCTS,
    payload: Array<IOrderProducts>
}

export type ProfileAction = GetProfileAction | GoogleConnectSuccessAction | FacebookConnectSuccessAction;
export type EmailConfirmAction = ConfirmEmailAction | SendConfirmEmailAction | IsEmailConfirmedAction;
export type OrderAction = GetOrderProductsAction;