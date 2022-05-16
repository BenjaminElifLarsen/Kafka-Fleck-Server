<template>
  <div class="ui segment statistic" style="width:100%;">
        <div class="value">
            {{total_formatted}}
        </div>
        <div class="label">
            Total
        </div>
    </div>
</template>

<script>
export default {
    name: 'total-display',
    props : {
        total : Number
    },
    computed : {
        total_formatted() {
            return (this.total.toLocaleString('da-DK', {
    style: 'currency', 
    currency: 'DKK', 
    minimumFractionDigits: 2 
}));
        }
    }
}
</script>

<style>

</style>