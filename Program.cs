//*****************************************************************************
//** 1671. Minimum Number of Removals to Make Mountain Array    leetcode     **
//*****************************************************************************


int lower_bound(int* arr, int size, int value) {
    int left = 0, right = size;
    while (left < right) {
        int mid = (left + right) / 2;
        if (arr[mid] < value)
            left = mid + 1;
        else
            right = mid;
    }
    return left;
}

int max(int a, int b) {
    return (a > b) ? a : b;
}

int minimumMountainRemovals(int* nums, int numsSize) {
    int* left_lis_len = (int*)malloc(numsSize * sizeof(int));
    int* right_lis_len = (int*)malloc(numsSize * sizeof(int));
    int* lis = (int*)malloc(numsSize * sizeof(int));
    int lis_size = 0;

    // Calculate left LIS lengths
    for (int i = 0; i < numsSize; ++i) {
        int pos = lower_bound(lis, lis_size, nums[i]);
        left_lis_len[i] = pos + 1;  // store length of LIS ending at i
        if (pos == lis_size) {
            lis[lis_size++] = nums[i];
        } else {
            lis[pos] = nums[i];
        }
    }

    // Reset lis and lis_size for right LIS calculation
    lis_size = 0;
    int max_len = 0;

    // Calculate right LIS lengths
    for (int i = numsSize - 1; i >= 0; --i) {
        int pos = lower_bound(lis, lis_size, nums[i]);
        right_lis_len[i] = pos + 1;  // store length of LIS starting at i
        if (pos == lis_size) {
            lis[lis_size++] = nums[i];
        } else {
            lis[pos] = nums[i];
        }
    }

    // Find maximum length of a mountain
    for (int i = 1; i < numsSize - 1; ++i) {
        if (left_lis_len[i] > 1 && right_lis_len[i] > 1) {
            max_len = max(max_len, left_lis_len[i] + right_lis_len[i] - 1);
        }
    }

    free(left_lis_len);
    free(right_lis_len);
    free(lis);

    return numsSize - max_len;
}