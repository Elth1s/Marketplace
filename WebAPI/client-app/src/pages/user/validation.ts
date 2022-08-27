import * as Yup from 'yup';


export const ProfileSchema = Yup.object().shape({
    firstName: Yup.string().min(2).max(15).required().label('First Name'),
    secondName: Yup.string().min(2).max(40).required().label('Second Name')
});

export const OrderSchema = Yup.object().shape({
    consumerFirstName: Yup.string().min(2).max(15).required().label('First Name'),
    consumerSecondName: Yup.string().min(2).max(40).required().label('Second Name'),
    consumerEmail: Yup.string().email().label('Email'),
    consumerPhone: Yup.string().phone().required().label('Phone'),
});