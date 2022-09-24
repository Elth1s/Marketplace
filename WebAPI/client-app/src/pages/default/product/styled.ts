import { Box, Divider, Typography, Rating, Button, ListItem, List, Grid, styled } from '@mui/material';

export const PriceBox = styled(Box)(() => ({
    display: "flex",
    flexDirection: "column",
    justifyContent: "center",
    alignItems: "flex-start",
    marginTop: "70px"
}))

export const RatingBox = styled(Box)(() => ({
    display: "flex",
    mb: "45px"
}))

export const CharacteristicGrid = styled(Grid)(() => ({
    position: "relative",
    marginBottom: "60px",
}))

export const CharacteristicTypography = styled(Typography)(() => ({
    background: "#FFF",
}))

export const CharacteristicDivider = styled(Divider)(({theme}) => ({
    position: "absolute",
    height: "1px",
    width: "100%",
    top: "20px",
    borderColor: theme.palette.mode == "dark" ? "#FFF" : "#000",
    borderStyle: "dotted",
    borderBottomWidth: "revert",
    zIndex: "-1",
}))

export const SellerContactsButton = styled(Button)(() => ({
    fontSize: "20px",
    fontWeight: "medium",
    textTransform: "none",
    borderRadius: "10px",
    borderWidth: "4px",
    borderColor: "#0E7C3A",
    lineHeight: "25px",
    padding: "13.5px 20px",
    "&:hover": {
        borderWidth: "4px",
    },
    "&& .MuiTouchRipple-child": {
        borderRadius: "4px",
    }
}))

export const BuyButton = styled(Button)(() => ({
    fontSize: "20px",
    textTransform: "none",
    fontWeight: "medium",
    borderRadius: "10px",
    padding: "10px 20px",
    "&>*:nth-of-type(1)": {
        marginRight: "20px",
        marginLeft: "0px"
    }
}))

export const BuyButtonSecondStyle = styled(Button)(() => ({
    fontSize: "27px",
    lineHeight: "35px",
    textTransform: "none",
    borderRadius: "10px",
    paddingTop: "15px",
    paddingBottom: "15px",
    "&>*:nth-of-type(1)": {
        marginRight: "20px",
        marginLeft: "0px"
    }
}))

export const SellerContactsButtonSecondStyle = styled(Button)(() => ({
    fontSize: "27px",
    lineHeight: "35px",
    textTransform: "none",
    borderRadius: "10px",
    borderWidth: "4px",
    borderColor: "#0E7C3A",
    paddingTop: "13.5px",
    paddingBottom: "13.5px",
    "&:hover": {
        borderWidth: "4px",
    },
    "&& .MuiTouchRipple-child": {
        borderRadius: "4px",
    }
}))

export const ListStyle = styled(List)(() => ({
    borderRadius: "10px",
    border: "1px solid #7e7e7e",
    padding: "49px 67px",
    marginTop: "30px",
    "&>*:nth-of-type(1)": {
        marginTop: "0px"
    }
}))

export const ListItemStyle = styled(ListItem)(() => ({
    padding: "0px",
    marginTop: "40px"
}))