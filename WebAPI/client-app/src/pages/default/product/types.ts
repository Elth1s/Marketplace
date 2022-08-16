export enum ProductActionTypes {
    GET_PRODUCT_BY_URLSLUG = "GET_PRODUCT_BY_URLSLUG",
    GET_SIMILAR_PRODUCTS = "GET_SIMILAR_PRODUCTS",
    UPDATE_SELECTED_PRODUCT = "UPDATE_SELECTED_PRODUCT",
}

export enum ReviewActionTypes {
    GET_REVIEWS = "GET_REVIEWS",
    GET_REVIEW_BY_ID = "GET_REVIEW_BY_ID",
    GET_MORE_REVIEWS = "GET_MORE_REVIEWS",
    GET_REVIEW_REPLIES = "GET_REVIEW_REPLIES",
    GET__MORE_REVIEW_REPLIES = "GET__MORE_REVIEW_REPLIES"
}
export enum QuestionActionTypes {
    GET_QUESTIONS = "GET_QUESTIONS",
    GET_QUESTION_BY_ID = "GET_QUESTION_BY_ID",
    GET_MORE_QUESTIONS = "GET_MORE_QUESTIONS",
    GET_QUESTION_REPLIES = "GET_QUESTION_REPLIES",
    GET__MORE_QUESTION_REPLIES = "GET__MORE_QUESTION_REPLIES"
}

export interface IParentCategoryItem {
    name: string,
    image: string,
    urlSlug: string
}

export interface IFilterItem {
    value: string,
    filterName: string,
    unitMeasure: string
}

export interface ISimilarProduct {
    name: string,
    image: string,
    price: number,
    statusName: string,
    urlSlug: string,
    discount: number | null
}

export interface IProductImage {
    id: number,
    name: string,
    rating: number
}

export interface IProductItem {
    isInBasket: boolean,
    name: string,
    shopId: number,
    shopName: string,
    productStatus: string,
    images: Array<IProductImage>,
    price: number,
    filters: Array<IFilterItem>
}

//Review
export interface IReviewItem {
    id: number,
    fullName: string,
    productRating: number,
    date: string,
    advantages: string,
    disadvantages: string,
    comment: string,
    videoURL: string,
    isLiked: boolean,
    isDisliked: boolean,
    dislikes: number,
    likes: number,
    replies: number,
    images: Array<string>
}
export interface IReview {
    fullName: string,
    email: string
    productRating: number,
    advantages: string,
    disadvantages: string,
    comment: string,
    videoURL: string,
    productSlug: string
    images: Array<string>
}

//Review Reply
export interface IReviewReplyItem {
    id: number,
    fullName: string,
    date: string,
    text: string,
    isSeller: boolean
}
export interface IReviewReply {
    fullName: string,
    email: string,
    text: string,
    reviewId: number
}
//Question
export interface IQuestionItem {
    id: number,
    fullName: string,
    date: string,
    message: string,
    isLiked: boolean,
    isDisliked: boolean,
    dislikes: number,
    likes: number,
    replies: number,
    images: Array<string>
}
export interface IQuestion {
    fullName: string,
    email: string
    message: string,
    productSlug: string
    images: Array<number>
}

//Question Reply
export interface IQuestionReplyItem {
    id: number,
    fullName: string,
    date: string,
    text: string,
    isSeller: boolean
}
export interface IQuestionReply {
    fullName: string,
    email: string,
    text: string,
    questionId: number
}

export interface ProductState {
    parents: Array<IParentCategoryItem>,
    product: IProductItem,
    similarProducts: Array<ISimilarProduct>,
    reviews: Array<IReviewItem>,
    reviewsCount: number
}

export interface IProductWithParents {
    product: IProductItem,
    parents: Array<IParentCategoryItem>,
}

export interface IReviewWithCount {
    values: Array<IReviewItem>,
    count: number,
}

// Product actions

export interface GetProductByUrlSlugAction {
    type: ProductActionTypes.GET_PRODUCT_BY_URLSLUG,
    payload: IProductWithParents
}

export interface GetSimilarProductsAction {
    type: ProductActionTypes.GET_SIMILAR_PRODUCTS,
    payload: Array<ISimilarProduct>
}

export interface UpdateSelectedProductAction {
    type: ProductActionTypes.UPDATE_SELECTED_PRODUCT,
}

//ReviewAction

export interface GetReviewsAction {
    type: ReviewActionTypes.GET_REVIEWS,
    payload: IReviewWithCount
}

export interface GetMoreReviewsAction {
    type: ReviewActionTypes.GET_MORE_REVIEWS,
    payload: Array<IReviewItem>
}

export type ProductAction = GetProductByUrlSlugAction |
    GetSimilarProductsAction |
    UpdateSelectedProductAction;

export type ReviewAction = GetReviewsAction |
    GetMoreReviewsAction;

// export type QuestionAction = GetProductByUrlSlugAction |
//     GetSimilarProductsAction |
//     UpdateSelectedProductAction;