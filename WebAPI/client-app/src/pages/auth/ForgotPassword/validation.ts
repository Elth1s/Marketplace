import * as Yup from 'yup';
import "yup-phone";

const passwordRegExp = /^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-_]).{8,}$/


export const ResetPasswordEmailSchema = Yup.object().shape({
    email: Yup.string().email().required().label('Email'),
});

export const ResetPasswordPhoneSchema = Yup.object().shape({
    phone: Yup.string().phone().required().label('Phone')
});

export const ResetChangePasswordSchema = Yup.object().shape({
    password: Yup.string().matches(passwordRegExp, 'Password is not valid').required().label('Password'),
    confirmPassword: Yup.string()
        .oneOf([Yup.ref('password'), null], 'Passwords must match').required('Confirm password is required')
});