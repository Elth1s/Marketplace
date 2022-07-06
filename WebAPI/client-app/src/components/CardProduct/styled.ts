import Card from '@mui/material/Card';
import CardContent from '@mui/material/CardContent';
import Typography from "@mui/material/Typography";

import { styled } from '@mui/material';


export const CardStyle = styled(Card)(({ theme }) => ({
    border: "1px solid #000",
    borderRadius: "10px",
    padding: "25px 20px",
}));

export const CardContentStyle = styled(CardContent)(({ theme }) => ({
    padding: "5px 0px 0px",
    "&:last-child":{
        paddingBottom: "0px"
    }
}));

export const CardTitleStyle = styled(Typography)(({ theme}) => ({
    color: "#000",
    fontSize: "18px",
    paddingBottom: "10px",
}))

export const CardStatusStyle = styled(Typography)(({ theme}) => ({
    color: theme.palette.secondary.main,
    fontSize: "16px",
    paddingBottom: "35px",
}))

export const CardPriceStyle = styled(Typography)(({ theme}) => ({
    color: "#000",
    fontSize: "20px",
}))
