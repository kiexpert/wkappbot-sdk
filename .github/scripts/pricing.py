"""pricing.py — Single source of truth for WKAppBot license pricing.

Import this wherever tier/days calculations are needed.
"""
import math

# Rate: $100 = 30 days (~$3.33/day)
DAYS_PER_100 = 30

# Sudo threshold (USD)
SUDO_THRESHOLD = 363  # $363 → 108 days Sudo

# Products (for reference / UI display)
PRODUCTS = [
    # (name, tier, usd, days, description)
    ("CDP  1개월",  "cdp",  100, 30,  "기본 자동화 기능 1개월"),
    ("CDP  3개월",  "cdp",  270, 81,  "기본 자동화 기능 3개월"),
    ("CDP  6개월",  "cdp",  540, 162, "기본 자동화 기능 6개월"),
    ("Sudo 3개월",  "sudo", 363, 108, "전체 기능 해제 3개월"),
    ("Sudo 6개월",  "sudo", 726, 217, "전체 기능 해제 6개월"),
    ("Sudo 12개월", "sudo", 1452, 435, "전체 기능 해제 12개월"),
]


def tier_from_amount(amount: float) -> str:
    return "sudo" if amount >= SUDO_THRESHOLD else "cdp"


def calc_days(amount: float, license_type: str = "one_time") -> int:
    if license_type == "monthly":
        return 30
    return max(1, math.floor(amount * DAYS_PER_100 / 100))
