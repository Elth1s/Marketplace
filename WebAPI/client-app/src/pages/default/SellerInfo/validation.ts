import * as Yup from 'yup';

export const shopReviewValidationFields = Yup.object().shape({
    fullName: Yup.string().required().min(2).max(60).label('Full name'),
    email: Yup.string().required().label('Email'),
    comment: Yup.string().max(450).label('Comment'),
});