import Box from "@mui/material/Box";
import Container from "@mui/material/Container";
import Grid from "@mui/material/Grid";

import PhoneIcon from '@mui/icons-material/Phone';
import LocationOnIcon from '@mui/icons-material/LocationOn';
import ShoppingCartIcon from '@mui/icons-material/ShoppingCart';

import FacebookIcon from '@mui/icons-material/Facebook';
import TelegramIcon from '@mui/icons-material/Telegram';
import InstagramIcon from '@mui/icons-material/Instagram';

import { ItemBoxStyled, TitleStyle, BodyStyle, LinkStyle } from "./styled";
import { white_shopping_cart, white_map_pin, phone_call, icon_facebook, icon_instagram, icon_telegram, icon_viber } from "../../assets/icons";

const about = [
    {
        icon: <img
            style={{ width: "30px", height: "30px", marginRight: "10px" }}
            src={phone_call}
            alt="icon"
        />, text: "+38054 824 67 31"
    },
    {
        icon: <img
            style={{ width: "30px", height: "30px", marginRight: "10px" }}
            src={white_map_pin}
            alt="icon"
        />, text: "Ukraine, str. Soborna 27"
    },
    {
        icon: <img
            style={{ width: "30px", height: "30px", marginRight: "10px" }}
            src={white_shopping_cart}
            alt="icon"
        />, text: "10:00 - 21:00 (No weekends)"
    },
]

const links = [
    {
        title: 'Services and services',
        description: ['Service agreements', 'Gift certificates', 'Credit and payment in installments', 'Gift cards'],
    },
    {
        title: 'Partners',
        description: ['Shafa', 'Crafta.ua', 'Vsisvoi.ua', 'Izi.ua'],
    },
    {
        title: 'Assistance',
        description: ['Find an order', 'Guarantee', 'Service centers', 'Frequently asked questions'],
    },
];

const Footer = () => {
    return (
        <Box component="footer" sx={{ backgroundColor: "secondary.main", marginTop: "auto", py: "110px" }}>
            <Container sx={{ maxWidth: { xl: "xl", lg: "lg", md: "md" } }}>
                <Grid container justifyContent="space-between">
                    <Grid item >
                        <TitleStyle variant="h4" sx={{ marginBottom: "30px" }}>About us</TitleStyle>
                        <Box sx={{ display: "flex", marginBottom: "45px" }}>
                            <img
                                style={{ width: "30px", height: "30px", marginRight: "20px" }}
                                src={icon_facebook}
                                alt="icon"
                            />
                            <img
                                style={{ width: "30px", height: "30px", marginRight: "20px" }}
                                src={icon_viber}
                                alt="icon"
                            />
                            <img
                                style={{ width: "30px", height: "30px", marginRight: "20px" }}
                                src={icon_telegram}
                                alt="icon"
                            />
                            <img
                                style={{ width: "30px", height: "30px", marginRight: "20px" }}
                                src={icon_instagram}
                                alt="icon"
                            />
                        </Box>
                        {about.map((item, index) => (
                            <ItemBoxStyled key={index} display="flex" alignItems="center">
                                {item.icon}
                                <BodyStyle variant="h4">{item.text}</BodyStyle>
                            </ItemBoxStyled>
                        ))}
                    </Grid>
                    {links.map((link, index) => (
                        <Grid key={index} >
                            <TitleStyle variant="h4" sx={{ marginBottom: "60px" }}>{link.title}</TitleStyle>
                            {link.description.map((item, index) => (
                                <ItemBoxStyled key={index} display="flex">
                                    <LinkStyle href="#" variant="h4">{item}</LinkStyle>
                                </ItemBoxStyled>
                            ))}
                        </Grid>
                    ))}
                </Grid>
            </Container>
        </Box>
    );
}

export default Footer;