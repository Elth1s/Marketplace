import Container from '@mui/material/Container';
import Button from '@mui/material/Button';
import Typography from '@mui/material/Typography';

import Header from '../../containers/DefaultLayout/Header';

const NotFound = () => {
    return (
        <>
            <Header />
            <Container component="main" sx={{
                maxWidth: { xl: "xl", lg: "lg", md: "md" },
                marginTop: "153px",
                textAlign: "center",
            }}>
                <Typography sx={{ fontSize: "150px", fontWeight: "600", lineHeight: "188px", marginBottom: "20px" }}>404</Typography>
                <Typography variant="h1" sx={{ marginBottom: "20px" }}>Page not found</Typography>
                <Typography variant="h4" sx={{ fontWeight: "500", marginBottom: "95px" }}>
                    Unfortunately, the page you asked for could not be found.
                    <br />
                    Please go back to the main page.
                </Typography>
                <Button
                    variant="contained"
                    href="/"
                    sx={{
                        fontWeight: "500",
                        fontSize: "20px",
                        lineHeight: "25px",
                        borderRadius: "10px",
                        px: "65px",
                        py: "15px",
                        textTransform: "capitalize",
                    }}>
                    Page home
                </Button>
            </ Container>
        </>
    );
}

export default NotFound;