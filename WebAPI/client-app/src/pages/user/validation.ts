import * as Yup from 'yup';

const passwordRegExp = /^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-_]).{8,}$/

export const ProfileSchema = Yup.object().shape({
    firstName: Yup.string().min(2).max(15).required().label('First Name'),
    secondName: Yup.string().min(2).max(40).required().label('Second Name'),
    userName: Yup.string().min(2).max(40).required().label('User Name'),
});

export const ResetPasswordSchema = Yup.object().shape({
    email: Yup.string().email().required().label('Email'),
});

export const ResetChangePasswordSchema = Yup.object().shape({
    password: Yup.string().matches(passwordRegExp, 'Password is not valid').required().label('Password'),
    confirmPassword: Yup.string()
        .oneOf([Yup.ref('password'), null], 'Passwords must match').required('Confirm password is required')
});