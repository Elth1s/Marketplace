import * as Yup from 'yup';

export const CreateShopSchema = Yup.object().shape({
    // firstName: Yup.string().min(3).max(50).required().label('First Name'),
    // secondName: Yup.string().min(3).max(75).required().label('Second Name'),
    // emailOrPhone: Yup.string().required().label('Email or phone'),
});