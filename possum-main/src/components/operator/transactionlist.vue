<template>
  <div class="ui middle aligned divided list " id="transactions" >
      <div class="item" v-for="item in list" v-bind:key="item.no">
        <div class="right floated content">{{item.price}}</div>
        <div class="content">{{item.count}} x {{item.name}}</div>
        
      </div>
  </div>
</template>

<script>
export default {
    props: ["list"]
}
</script>

<style>
#transactions .item{
  padding-top:8px;
  padding-bottom:8px;
  
}
</style>