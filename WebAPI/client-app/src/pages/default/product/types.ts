export enum ProductActionTypes {
    GET_PRODUCT_BY_URLSLUG = "GET_PRODUCT_BY_URLSLUG",
    GET_PRODUCT_RATING_BY_URL_SLUG = "GET_PRODUCT_RATING_BY_URL_SLUG",
    UPDATE_IS_IN_CART = "UPDATE_IS_IN_CART",
    UPDATE_IS_SELECTED = "UPDATE_IS_SELECTED",
}

export enum ReviewActionTypes {
    GET_REVIEWS = "GET_REVIEWS",
    GET_REVIEW_BY_ID = "GET_REVIEW_BY_ID",
    GET_MORE_REVIEWS = "GET_MORE_REVIEWS",
    GET_REVIEW_REPLIES = "GET_REVIEW_REPLIES",
    GET__MORE_REVIEW_REPLIES = "GET__MORE_REVIEW_REPLIES",
    CHANGE_REVIEW_LIKE = "CHANGE_REVIEW_LIKE",
    CHANGE_REVIEW_DISLIKE = "CHANGE_REVIEW_DISLIKE",
    GET_REPLIES_FOR_REVIEW = "GET_REPLIES_FOR_REVIEW",
    GET_MORE_REPLIES_FOR_REVIEW = "GET_MORE_REPLIES_FOR_REVIEW"
}
export enum QuestionActionTypes {
    GET_QUESTIONS = "GET_QUESTIONS",
    GET_QUESTION_BY_ID = "GET_QUESTION_BY_ID",
    GET_MORE_QUESTIONS = "GET_MORE_QUESTIONS",
    GET_QUESTION_REPLIES = "GET_QUESTION_REPLIES",
    GET__MORE_QUESTION_REPLIES = "GET__MORE_QUESTION_REPLIES",
    CHANGE_QUESTION_LIKE = "CHANGE_QUESTION_LIKE",
    CHANGE_QUESTION_DISLIKE = "CHANGE_QUESTION_DISLIKE",
    GET_REPLIES_FOR_QUESTION = "GET_REPLIES_FOR_QUESTION",
    GET_MORE_REPLIES_FOR_QUESTION = "GET_MORE_REPLIES_FOR_QUESTION"
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
    isSelected: boolean,
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

export interface IProductRating {
    rating: number,
    countReviews: number,
}

export interface IDeliveryType {
    id: number,
    name: string,
    lightIcon: string,
    darkIcon: string
}

export interface IProductItem {
    isInBasket: boolean,
    isSelected: boolean,
    name: string,
    shopId: number,
    shopName: string,
    productStatus: string,
    shopRating: number,
    images: Array<IProductImage>,
    price: number,
    discount: number | null,
    filters: Array<IFilterItem>
    deliveryTypes: Array<IDeliveryType>
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
    repliesCount: number,
    replies: Array<IReplyItem>,
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
    repliesCount: number,
    replies: Array<IReplyItem>
    images: Array<string>
}
export interface IQuestion {
    fullName: string,
    email: string
    message: string,
    productSlug: string
    images: Array<string>
}

//Reply
export interface IReplyItem {
    id: number,
    fullName: string,
    date: string,
    text: string,
    isSeller: boolean
}

export interface IReply {
    fullName: string,
    email: string,
    text: string,
}

export interface IReplyWithCount {
    id: number,
    count: number,
    values: Array<IReplyItem>
}


export interface ProductState {
    parents: Array<IParentCategoryItem>,
    product: IProductItem,
    productRating: IProductRating,
    reviews: Array<IReviewItem>,
    reviewsCount: number,
    questions: Array<IQuestionItem>,
    questionsCount: number
}

export interface IProductWithParents {
    product: IProductItem,
    parents: Array<IParentCategoryItem>,
}

export interface IReviewWithCount {
    values: Array<IReviewItem>,
    count: number,
}

export interface IQuestionWithCount {
    values: Array<IQuestionItem>,
    count: number,
}

// Product actions

export interface GetProductByUrlSlugAction {
    type: ProductActionTypes.GET_PRODUCT_BY_URLSLUG,
    payload: IProductWithParents
}
export interface GetProductRatingByUrlSlugAction {
    type: ProductActionTypes.GET_PRODUCT_RATING_BY_URL_SLUG,
    payload: IProductRating
}

export interface UpdateIsInCartAction {
    type: ProductActionTypes.UPDATE_IS_IN_CART,
}

export interface UpdateIsSelectedAction {
    type: ProductActionTypes.UPDATE_IS_SELECTED,
}

//ReviewAction

export interface GetReviewsAction {
    type: ReviewActionTypes.GET_REVIEWS,
    payload: IReviewWithCount
}

export interface GetMoreReviewsAction {
    type: ReviewActionTypes.GET_MORE_REVIEWS,
    payload: IReviewWithCount
}

export interface ChangeReviewLikeAction {
    type: ReviewActionTypes.CHANGE_REVIEW_LIKE,
    payload: number
}

export interface ChangeReviewDislikeAction {
    type: ReviewActionTypes.CHANGE_REVIEW_DISLIKE,
    payload: number
}

export interface GetRepliesForReviewAction {
    type: ReviewActionTypes.GET_REPLIES_FOR_REVIEW,
    payload: IReplyWithCount
}

export interface GetMoreRepliesForReviewAction {
    type: ReviewActionTypes.GET_MORE_REPLIES_FOR_REVIEW,
    payload: IReplyWithCount
}

//QuestionAction

export interface GetQuestionsAction {
    type: QuestionActionTypes.GET_QUESTIONS,
    payload: IQuestionWithCount
}

export interface GetMoreQuestionsAction {
    type: QuestionActionTypes.GET_MORE_QUESTIONS,
    payload: IQuestionWithCount
}

export interface ChangeQuestionLikeAction {
    type: QuestionActionTypes.CHANGE_QUESTION_LIKE,
    payload: number
}

export interface ChangeQuestionDislikeAction {
    type: QuestionActionTypes.CHANGE_QUESTION_DISLIKE,
    payload: number
}

export interface GetRepliesForQuestionAction {
    type: QuestionActionTypes.GET_REPLIES_FOR_QUESTION,
    payload: IReplyWithCount
}

export interface GetMoreRepliesForQuestionAction {
    type: QuestionActionTypes.GET_MORE_REPLIES_FOR_QUESTION,
    payload: IReplyWithCount
}


export type ProductAction = GetProductByUrlSlugAction |
    GetProductRatingByUrlSlugAction |
    UpdateIsInCartAction |
    UpdateIsSelectedAction;

export type ReviewAction = GetReviewsAction |
    GetMoreReviewsAction |
    ChangeReviewDislikeAction |
    ChangeReviewLikeAction |
    GetMoreRepliesForReviewAction |
    GetRepliesForReviewAction;

export type QuestionAction = GetQuestionsAction |
    GetMoreQuestionsAction |
    ChangeQuestionLikeAction |
    ChangeQuestionDislikeAction |
    GetRepliesForQuestionAction |
    GetMoreRepliesForQuestionAction;