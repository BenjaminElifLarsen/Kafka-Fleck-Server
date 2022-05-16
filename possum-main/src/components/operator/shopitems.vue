<template>
    <div>
        <positem    v-for="item in inStockItems" 
                    v-bind:item="item"
                    v-bind:key="item.id"
                    @click="$emit('itemclick',item)">
        </positem>
    </div>
</template>

<script>
import positem from './positem.vue'
export default {
    components : { positem },
    props : ['buyableItems'],
    computed : {
        inStockItems : function() {
            var instock = []
            this.buyableItems.forEach( function(item) {
                if (item.stock > 0 || item.stock == "") {

                    instock.push(item);
                }
            });
            return instock;
        }
    }
}
</script>

<style>

</style>