import { Box, Divider, Typography, Rating, styled } from '@mui/material';

export const PriceBox = styled(Box)(() => ({
    display: "flex",
    flexDirection: "column",
    justifyContent: "center",
    alignItems: "flex-start"
}))

export const RatingBox = styled(Box)(({ theme }) => ({
    display: "flex",
    mb: "45px"
}))

export const CharacteristicBox = styled(Box)(({ theme }) => ({
    position: "relative",
    display: "flex",
    justifyContent: "space-between",
    marginBottom: "60px",
}))

export const CharacteristicTypography = styled(Typography)(() => ({
    background: "#FFF",
}))

export const CharacteristicDivider = styled(Divider)(() => ({
    position: "absolute",
    height: "1px",
    width: "100%",
    bottom: "5px",
    borderColor: "#000000",
    borderStyle: "dotted",
    borderBottomWidth: "revert",
    zIndex: "-1",
}))

export const RatingStyle = styled(Rating)(({ theme }) => ({
    '& .MuiRating-iconFilled': {
        color: theme.palette.primary.main,
    },
    '& .MuiRating-iconHover': {
        color: theme.palette.primary.main,
    },
}))