import { LoadingButton } from "@mui/lab";
import { styled } from "@mui/material";


export const LoadingButtonStyled = styled(LoadingButton)(({ theme }) => ({
    backgroundColor: `${theme.palette.secondary.main}`,
    "&:hover": {
        backgroundColor: `${theme.palette.secondary.dark}`,
    },
}));