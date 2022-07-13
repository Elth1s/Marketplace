import * as Yup from 'yup';

export const validationFields = Yup.object().shape({
    name: Yup.string().required().label('User Name'),
    email: Yup.string().required().label('User Email'),
    rating: Yup.number().required().label('Rating'),
    advantages: Yup.string().required().label('Advantages'),
    disadvantages: Yup.string().required().label('Disadvantages'),
    review: Yup.string().required().label('Review'),
});