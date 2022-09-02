import * as Yup from 'yup';

export const reviewValidationFields = Yup.object().shape({
    fullName: Yup.string().required().min(2).max(60).label('Full name'),
    email: Yup.string().required().label('Email'),
    advantages: Yup.string().max(100).label('Advantages'),
    disadvantages: Yup.string().max(100).label('Disadvantages'),
    comment: Yup.string().required().min(1).max(850).label('Comment'),
});

export const questionValidationFields = Yup.object().shape({
    fullName: Yup.string().required().min(2).max(80).label('Full name'),
    email: Yup.string().required().label('Email'),
    message: Yup.string().required().min(2).max(500).label('Message'),
});