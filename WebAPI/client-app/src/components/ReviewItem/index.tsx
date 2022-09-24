import { CachedOutlined, StarRounded } from '@mui/icons-material'
import {
    Box,
    Button,
    IconButton,
    Typography,
    useTheme
} from '@mui/material'
import { FC, useEffect, useState } from 'react'
import { useTranslation } from 'react-i18next'
import {
    selected_dislike, selected_like,
    white_dislike, black_dislike,
    white_like, black_like
} from '../../assets/icons'
import { useActions } from '../../hooks/useActions'
import { ShowMoreButton } from '../../pages/default/Catalog/styled'
import AddReply from '../../pages/default/product/AddReply'
import { IReply, IReplyItem } from '../../pages/default/product/types'
import LinkRouter from '../LinkRouter'
import { RatingStyle } from '../Rating/styled'

interface Props {
    id: number,
    reviewLink: string,
    fullName: string,
    date: string,
    productRating: number,
    comment: string,
    advantages: string,
    disadvantages: string,
    images: Array<string>,
    videoURL: string,
    isLiked: boolean,
    isDisliked: boolean,
    likes: number,
    dislikes: number,
    repliesCount: number,
    replies: Array<IReplyItem>,
    getData: any
}

const ReviewItem: FC<Props> = ({ id, reviewLink, fullName, date, productRating, comment, advantages, disadvantages, images, videoURL, isLiked, isDisliked, likes, dislikes, repliesCount, replies, getData }) => {
    const { t } = useTranslation();
    const { palette } = useTheme();

    const { ReviewLike, ReviewDislike, AddReviewReply, GetRepliesForReview, GetMoreRepliesForReview } = useActions();

    const [isReplyOpen, setIsReplyOpen] = useState<boolean>(false);
    const [page, setPage] = useState<number>(1);
    const [rowsPerPage, setRewsPerPage] = useState<number>(3);

    const changeLiked = async () => {
        await ReviewLike(id)
    }

    const changeDisliked = async () => {
        await ReviewDislike(id)
    }

    const AddReplyForReview = async (value: IReply) => {
        await AddReviewReply(value, id);
        await GetRepliesForReview(id, 1, rowsPerPage)
        setPage(1);
    }

    const GetReplies = async () => {
        await GetRepliesForReview(id, page, rowsPerPage);
        setIsReplyOpen(!isReplyOpen);
    }

    const showMore = async () => {
        try {
            let newPage = page + 1;
            await GetMoreRepliesForReview(id, newPage, rowsPerPage)
            setPage(newPage);
        } catch (ex) {
        }
    }


    return (
        <Box sx={{ border: "1px solid #7e7e7e", borderRadius: "10px", p: "37px 41px", mb: "20px" }}>
            <Box sx={{ display: "flex", justifyContent: "space-between" }}>
                <Typography variant="h1" color="inherit">
                    {fullName}
                </Typography>
                <Box sx={{ display: "flex", alignItems: "center" }}>
                    {/* <LinkRouter to={reviewLink} underline="none" color="inherit">
                        <IconButton sx={{ "&:hover": { background: "transparent" }, "&& .MuiTouchRipple-child": { backgroundColor: "transparent" } }}>
                            <img
                                style={{ width: "30px", height: "30px" }}
                                src={link}
                                alt="link"
                            />
                        </IconButton>
                    </LinkRouter> */}
                    <Typography variant="h5" color="inherit" sx={{ ml: "30px" }}>
                        {date}
                    </Typography>
                </Box>
            </Box>
            <Box sx={{ display: "flex", justifyContent: "end" }}>
                <RatingStyle
                    sx={{ fontSize: "30px" }}
                    value={productRating}
                    precision={0.5}
                    readOnly
                    icon={<StarRounded sx={{ fontSize: "30px" }} />}
                    emptyIcon={<StarRounded sx={{ fontSize: "30px" }} />}
                />
            </Box>
            <Typography variant="h4" color="inherit" sx={{ mt: "21px" }}>
                {comment}
            </Typography>
            {advantages != "" &&
                <>
                    <Typography variant="h4" color="inherit" fontWeight="bold" sx={{ mt: "25px" }}>
                        {t("components.reviewItem.advantages")}:
                    </Typography>
                    <Typography variant="h4" color="inherit" sx={{ mt: "10px" }}>
                        {advantages}
                    </Typography>
                </>
            }
            {disadvantages != "" &&
                <><Typography variant="h4" color="inherit" fontWeight="bold" sx={{ mt: "25px" }}>
                    {t("components.reviewItem.disadvantages")}:
                </Typography>
                    <Typography variant="h4" color="inherit" sx={{ mt: "10px", mb: "25px" }}>
                        {disadvantages}
                    </Typography>
                </>
            }
            {images.map((image, index) => {
                return (
                    <img
                        key={`review_image_${index}`}
                        style={{ width: "100px", height: "100px" }}
                        src={image}
                        alt="reviewImage"
                    />
                )
            })}
            <Box sx={{ display: "flex", justifyContent: "space-between", mt: "50px" }}>
                <Box>
                    <AddReply create={AddReplyForReview} />
                    {!isReplyOpen && (repliesCount != 0 && <Button
                        sx={{
                            color: "primary",
                            textTransform: "none",
                            fontSize: "18px",
                            "&:hover": { background: "transparent" },
                            "&& .MuiTouchRipple-child": { backgroundColor: "transparent" }
                        }}
                        onClick={GetReplies}
                    >
                        {t("components.reviewItem.showReply")} ({repliesCount})
                    </Button>)}
                </Box>
                <Box>
                    <Button
                        sx={{
                            color: isLiked ? palette.secondary.main : "inherit",
                            textTransform: "none",
                            fontSize: "18px",
                            mr: "35px",
                            "&:hover": { background: "transparent" },
                            "&& .MuiTouchRipple-child": { backgroundColor: "transparent" }
                        }}
                        onClick={changeLiked}
                        startIcon={
                            <img
                                style={{ width: "30px", height: "30px" }}
                                src={isLiked ? selected_like : (palette.mode == "dark" ? white_like : black_like)}
                                alt="likeIcon"
                            />
                        }
                    >
                        {likes}
                    </Button>
                    <Button
                        sx={{
                            color: isDisliked ? palette.secondary.main : "inherit",
                            textTransform: "none",
                            fontSize: "18px",
                            "&:hover": { background: "transparent" },
                            "&& .MuiTouchRipple-child": { backgroundColor: "transparent" }
                        }}
                        onClick={changeDisliked}
                        startIcon={
                            <img
                                style={{ width: "30px", height: "30px" }}
                                src={isDisliked ? selected_dislike : (palette.mode == "dark" ? white_dislike : black_dislike)}
                                alt="dislikeIcon"
                            />
                        }
                    >
                        {dislikes}
                    </Button>
                </Box>
            </Box>
            {isReplyOpen && <Box sx={{ px: "30px" }}>
                {replies?.length !== 0 && replies.map((reply, index) => {
                    console.log(reply)
                    return (
                        <Box sx={{ pt: "30px", pb: "15px" }}>
                            <Box sx={{ display: "flex", justifyContent: "space-between" }}>
                                <Typography variant="h1" color="inherit">
                                    {reply.fullName}
                                </Typography>
                                <Typography variant="h5" color="inherit">
                                    {reply.date}
                                </Typography>
                            </Box>
                            <Typography variant="h4" color="inherit" sx={{ pt: "20px" }}>
                                {reply.text}
                            </Typography>
                        </Box>
                    )
                })}
                {replies.length != repliesCount && <Box sx={{ display: "flex", justifyContent: "center" }}>
                    <ShowMoreButton onClick={showMore} startIcon={<CachedOutlined />}>
                        {t("pages.catalog.showMore")}
                    </ShowMoreButton>
                </Box>}
            </Box>}
        </Box>
    )
}

export default ReviewItem